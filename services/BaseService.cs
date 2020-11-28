using System;
using atividade_2.Data;
using atividade_2.models;

namespace atividade_2.services
{
  public class BaseService
  {
    private static Hotel _hotel = Persistence.GetInstance.GetBaseHotel();

    public void FindClient()
    {
      if (_hotel.Clients.Count == 0)
      {
        Console.WriteLine("Não existem clientes cadastrados!");
        Console.Write("\nPressione Enter...");
        Console.ReadKey();
        return;
      }

      string name = "";
      string dateOfBirth = "";

      Console.Write("\n > Digite o nome do cliente: ");
      try { name = Console.ReadLine(); } catch { }

      Console.Write("\n > Digite a data de nascimento (DD-MM-YYYY): ");
      try { dateOfBirth = Console.ReadLine(); } catch { }

      var client = _hotel.FindClient(name, dateOfBirth);

      if (client == null)
      {
        Console.WriteLine("");
        Console.WriteLine("Cliente não localizado");
        Console.Write("\nPressione Enter...");
        Console.ReadKey();
        return;
      }

      ShowDetailsClient(client);

      Console.ReadKey();
    }

    public Client RegisterClient()
    {
      string name = "";
      string dateOfBirth = "";
      string rg = "";
      string phone = "";
      string address = "";
      string neighborhood = "";
      string city = "";
      string state = "";

      Console.Write("\n > Digite o nome do cliente: ");
      try { name = Console.ReadLine(); } catch { }

      Console.Write("\n > Digite a data de nascimento: ");
      try { dateOfBirth = Console.ReadLine(); } catch { }

      Console.Write("\n > Digite o RG: ");
      try { rg = Console.ReadLine(); } catch { }

      Console.Write("\n > Digite o telefone: ");
      try { phone = Console.ReadLine(); } catch { }

      Console.Write("\n > Digite o endereço (ex. rua Quatro, nº 10): ");
      try { address = Console.ReadLine(); } catch { }

      Console.Write("\n > Digite o bairro: ");
      try { neighborhood = Console.ReadLine(); } catch { }

      Console.Write("\n > Digite a cidade: ");
      try { city = Console.ReadLine(); } catch { }

      Console.Write("\n > Digite o estado: ");
      try { state = Console.ReadLine(); } catch { }

      var client = new Client(
        name,
        dateOfBirth,
        rg,
        phone,
        address,
        neighborhood,
        city,
        state
      );

      _hotel.Clients.Add(client);
      Persistence.GetInstance.Save();

      return client;
    }

    private void ShowDetailsClient(Client client)
    {
      Console.Clear();
      Console.WriteLine("");
      Console.WriteLine("".PadRight(100, '_'));
      Console.WriteLine($"Nome: {client.Name}");
      Console.WriteLine($"Data de nascimento: {FormatDate(client.DateOfBirth)}");
      Console.WriteLine($"RG: {client.Rg}");
      Console.WriteLine($"Telefone: {client.Phone}");
      Console.WriteLine($"Endereço: {client.Address}");
      Console.WriteLine($"Bairro: {client.Neighborhood}");
      Console.WriteLine($"Cidade: {client.City}");
      Console.WriteLine($"Estado: {client.State}");
    }

    public void ShowClients()
    {
      if (_hotel.Clients.Count == 0)
      {
        Console.WriteLine("Não existem clientes cadastrados!");
        Console.Write("\nPressione Enter...");
        Console.ReadKey();
        return;
      }

      Console.Clear();
      Console.WriteLine("");
      Console.WriteLine("".PadRight(100, '_'));
      foreach (var client in _hotel.Clients)
      {
        Console.WriteLine(
          "Nome:".PadRight(49 - client.Name.Length, ' ') + client.Name + "|" +
          "Data de nascimento:".PadRight(40, ' ') + FormatDate(client.DateOfBirth)
        );
        Console.WriteLine("".PadRight(100, '_'));
      }

      Console.Write("\nPressione Enter...");
      Console.ReadKey();
    }

    private string FormatDate(DateTime? date)
    {
      if (!date.HasValue)
      {
        return "";
      }
      return date.Value.ToString("dd/MM/yyyy");
    }
  }
}