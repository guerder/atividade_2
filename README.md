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
2. [ ] Implemente as classes em C#, com os devidos elementos:
3. [ ] Utilize pelo menos três (3) Design Patterns visto em sala (à sua escolha).
4. [ ] Escreva um documento explicando a estrutura de seu programa, e onde e como foram aplicados os Patterns.

## Designer Patterns utilizados

- Singleton
- Iterator
- Composite

### SINGLETON

Utilizei para manter toda a lógica de salvar e carregar a instância de hotel. Além de não precisar replicar o código de serialização e deserialização eu pude fazer uso da instância globalmente.

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

### ITERATOR

### COMPOSITE
