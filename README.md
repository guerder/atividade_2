## Sistema de Reserva de Hotel

O objetivo do sistema de hotel é automatizar o funcionamento de uma pousada. Para isso o sistema deverá possuir as seguintes funcionalidades:

- <ins>Cadastro de reservas de quartos</ins>: o operador deve criar a reserva informando tipo de acomodação, dia de entrada, dia de saída. Através do nome e data de nascimento o atendente deve verificar se o cliente já é cadastrado. Caso não seja, deve-se cadastrar o cliente, que deve informar: nome, endereço, telefone, bairro, cidade, estado , data de nascimento e rg;

- <ins>Controle de gastos dos hóspedes</ins>: deve-se controlar dos gastos de cada quarto, os gastos podem ser de telefone, diária do hotel e alimentação (valores fixos por dia);

- <ins>Fechamento de conta</ins>: no momento do fechamento de conta, o sistema deve emitir uma nota com os valores gastos de telefone, diária de hotel e alimentação e o total geral incluindo 5% de serviços do hotel;

- <ins>Relatórios diários</ins>: os atendentes podem gerar relatórios diários dos gastos de cada quarto;

### Regras

- A diária do hotel varia de acordo com o tipo de acomodação, que pode ser: simples, dupla ou tripla;

- Os dados de acomodação, como: tipo de acomodação, número da acomodação, descrição e disponibilidade podem ser consultados no momento da reserva.

- O controle de depósito de reservas é controlado pelo sistema financeiro, que apenas informa o sistema de hotel em questão se o valor da reserva foi creditado e o valor creditado. [**FAKE**]

## Leia atentamente a descrição e elabore:

1. [x] Monte o Diagrama de Classes
2. [x] Implemente as classes em C#, com os devidos elementos:
3. [x] Utilize pelo menos três (3) Design Patterns visto em sala (à sua escolha).
4. [x] Escreva um documento explicando a estrutura de seu programa, e onde e como foram aplicados os Patterns.

## Design Patterns utilizados

- [Singleton](https://github.com/guerder/atividade_2#singleton)
- [Composite](https://github.com/guerder/atividade_2#composite)
- [Iterator](https://github.com/guerder/atividade_2#iterator)

### SINGLETON

Utilizei para manter toda a lógica de salvar e carregar a instância de hotel. Além de não precisar replicar o código de serialização e deserialização eu pude fazer uso da instância globalmente.

#### Implementação

```
public  class  Persistence
{
  // O campo para armazenar a instância singleton
  private  static  Persistence _instance;
  private  Hotel _hotel;

  // Construtor protegido, chamado apenas internamente.
  protected  Persistence()
  {
    _hotel = new  Hotel();
    try
    {
      FileStream  fs = new  FileStream("data.bin", FileMode.Open);
      BinaryFormatter  bf = new  BinaryFormatter();
      _hotel = (Hotel)bf.Deserialize(fs);
      fs.Close();
    }
    catch { }
  }

  // Lógica para manter apenas uma instância da classe
  public  static  Persistence  GetInstance
  {
    get
    {
      if (_instance == null)
      {
        _instance = new  Persistence();
      }
      return  _instance;
      }
    }

  // Método para retornar a instância de Hotel pertencente a classe Persistence.
  public  Hotel  GetBaseHotel()
  {
    return  _hotel;
  }

  // Método com a lógica de serializar a instância de Hotel.
  public  void  Save()
  {
    try
    {
      FileStream  fs = new  FileStream("data.bin", FileMode.Create);
      BinaryFormatter  bf = new  BinaryFormatter();
      bf.Serialize(fs, _hotel);
      fs.Close();
    }
    catch (Exception  e)
    {
      Console.WriteLine("erro na gravação dos dados: " + e);
      Console.ReadKey();
    }
  }
}
```

### COMPOSITE

O menu da aplicação foi montado de forma hierárquica, como árvore, onde cada elemento do menu pode conter um grupo de sub-itens ou uma ação para ser invocada.

Esta é a hierarquia do menu seguida na aplicação:

![hierarquia do menu](https://raw.githubusercontent.com/guerder/atividade_2/master/assets/hierarquia_menu.png)

Abaixo está o diagrama de classe para a navegação no menu:

![diagrama de classe de navegação no menu](https://raw.githubusercontent.com/guerder/atividade_2/master/assets/composite.png)

#### Implementação

> MenuListing

```
public abstract class MenuListing
  {
    // Operações comuns de ambas as classes
    public abstract void Open();
    public string Name { get; set; }

    public void Titulo(string titulo)
    {
      Console.Clear();
      titulo = "".PadRight((100 - titulo.Length) / 2, ' ') + titulo.ToUpper();
      Console.WriteLine("".PadRight(100, ' '));
      Console.WriteLine(titulo);
      Console.WriteLine("".PadRight(100, ' '));
    }

  }
```

> ItemSimple

```
public class ItemSimple : MenuListing
  {
    private delegate void ActionItem();
    private ActionItem ActionExecute;

    public ItemSimple(string name, Action function)
    {
      this.Name = name;
      ActionExecute = new ActionItem(function);
    }

    // implementa a operação conforme especificidade da classe.
    public override void Open()
    {
      Titulo(Name);
      ActionExecute();
    }
  }
```

> Menu

```
public class Menu : MenuListing
  {
    private bool IsRoot;
    public Menu(string name, bool isRoot = false)
    {
      this.Name = name; this.IsRoot = isRoot;
    }
    public List<MenuListing> includedMenuListings = new List<MenuListing>();

    public void Add(MenuListing obj)
    {
      includedMenuListings.Add(obj);
    }

    // implementa a operação conforme especificidade da classe.
    public override void Open()
    {
      Titulo(Name);
      for (int i = 0; i < includedMenuListings.Count; i++)
      {
        var includedMenuListingsObject = includedMenuListings[i];
        Console.WriteLine($"{i + 1}. {includedMenuListingsObject.Name}");
      }

      if (!IsRoot)
      {
        Console.WriteLine("\n0. Voltar para o início");
      }

      int option = 0;
      Console.Write("\n > Digite o número correspondente: ");
      try
      {
        option = int.Parse(Console.ReadLine());
      }
      catch
      {
        option = -1;
      }

      if (option == 0 && !IsRoot)
      {
        return;
      }

      if ((option - 1) < 0 || (option) > includedMenuListings.Count)
      {
        Console.WriteLine("\n Opção inválida! Pressione Enter");
        Console.ReadKey();
        return;
      }
      var item = includedMenuListings[option - 1];

      item.Open();
    }
  }
```

> Client

```
class Program
  {
    private static BaseService baseService = new BaseService();
    static void Main(string[] args)
    {
      // Aqui está sendo montado a hierarquia do menu.
      Menu main = new Menu("Menu Principal", true);

      Menu register = new Menu("Cadastro");
      ItemSimple checkOut = new ItemSimple("Check-Out", baseService.CheckOut);
      ItemSimple table = new ItemSimple("Tabela de preços", baseService.TablePrices);
      ItemSimple report = new ItemSimple("Relatório diário", baseService.DailyReport);

      main.Add(register);
      main.Add(checkOut);
      main.Add(table);
      main.Add(report);

      Menu client = new Menu("Cliente");
      Menu reservation = new Menu("Reserva");
      Menu room = new Menu("Quarto");
      register.Add(client);
      register.Add(reservation);
      register.Add(room);

      ItemSimple findClient = new ItemSimple("Buscar Cliente", baseService.FindClient);
      ItemSimple registerClient = new ItemSimple("Cadastrar Cliente", baseService.RegisterClient);
      ItemSimple listClients = new ItemSimple("Listar Clientes", baseService.ShowClients);
      client.Add(findClient);
      client.Add(registerClient);
      client.Add(listClients);

      ItemSimple findReservation = new ItemSimple("Buscar Reserva", baseService.FindReservation);
      ItemSimple createReservation = new ItemSimple("Cadastrar Reserva", baseService.CreateReservation);
      ItemSimple addService = new ItemSimple("Adicionar Serviço", baseService.AddService);
      reservation.Add(findReservation);
      reservation.Add(createReservation);
      reservation.Add(addService);

      ItemSimple listRooms = new ItemSimple("Listar Quartos", () => baseService.ShowListRooms());
      room.Add(listRooms);

      do
      {
        main.Open();
      } while (true);
    }
  }
```

### ITERATOR

Em conjunto ao Design Pattern Composite utilizei de forma tímida o Iterator para implementar a exibição dos itens da classe Menu, de forma que a iteração ficasse oculta ao Client. O Client invoca o método Open() pertencente as classes ItemSimple e Menu, mas apenas em Menu é realizado a iteração.

#### implementação

> Menu

```
public class Menu : MenuListing
  {
    private bool IsRoot;
    public Menu(string name, bool isRoot = false)
    {
      this.Name = name; this.IsRoot = isRoot;
    }
    public List<MenuListing> includedMenuListings = new List<MenuListing>();

    public void Add(MenuListing obj)
    {
      includedMenuListings.Add(obj);
    }

    // implementa a operação conforme especificidade da classe.
    public override void Open()
    {
      Titulo(Name);
      for (int i = 0; i < includedMenuListings.Count; i++)
      {
        var includedMenuListingsObject = includedMenuListings[i];
        Console.WriteLine($"{i + 1}. {includedMenuListingsObject.Name}");
      }

      if (!IsRoot)
      {
        Console.WriteLine("\n0. Voltar para o início");
      }

      int option = 0;
      Console.Write("\n > Digite o número correspondente: ");
      try
      {
        option = int.Parse(Console.ReadLine());
      }
      catch
      {
        option = -1;
      }

      if (option == 0 && !IsRoot)
      {
        return;
      }

      if ((option - 1) < 0 || (option) > includedMenuListings.Count)
      {
        Console.WriteLine("\n Opção inválida! Pressione Enter");
        Console.ReadKey();
        return;
      }
      var item = includedMenuListings[option - 1];

      item.Open();
    }
  }
```
