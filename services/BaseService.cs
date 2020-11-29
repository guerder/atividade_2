using System;
using System.Linq;
using atividade_2.Data;
using atividade_2.models;
using atividade_2.models.enums;
using atividade_2.utils;

namespace atividade_2.services
{
  public class BaseService
  {
    private static Hotel _hotel = Persistence.GetInstance.GetBaseHotel();

    public void FindClient()
    {
      var client = findClient();

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

    private Client findClient()
    {
      if (_hotel.Clients.Count == 0)
      {
        Console.WriteLine("Não existem clientes cadastrados!");
        Console.Write("\nPressione Enter...");
        Console.ReadKey();
        return null;
      }

      string name = "";
      string dateOfBirth = "";

      Console.Write("\n > Digite o nome do cliente: ");
      try { name = Console.ReadLine(); } catch { }

      Console.Write("\n > Digite a data de nascimento (DD-MM-YYYY): ");
      try { dateOfBirth = Console.ReadLine(); } catch { }

      return _hotel.FindClient(name, dateOfBirth);
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

      Console.Write("\n > Digite a data de nascimento (DD-MM-YYYY): ");
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
      Console.WriteLine("");
      Console.WriteLine("".PadRight(100, '_'));
      Console.WriteLine($"Nome: {client.Name}");
      Console.WriteLine($"Data de nascimento: {Formatter.Date(client.DateOfBirth)}");
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
          "Data de nascimento:".PadRight(40, ' ') + Formatter.Date(client.DateOfBirth)
        );
        Console.WriteLine("".PadRight(100, '_'));
      }

      Console.Write("\nPressione Enter...");
      Console.ReadKey();
    }

    public void ShowListRooms(bool all = false)
    {
      Console.Clear();
      Console.WriteLine("");
      var listRooms = all ? _hotel.Rooms.Where(x => !x.isOcupedid) : _hotel.Rooms;
      foreach (var room in listRooms)
      {
        Console.WriteLine("".PadRight(100, '_'));
        Console.WriteLine(
          $"Nº: {room.RoomNumber}".PadRight(32, ' ') + "|" +
          $"Tipoº: {room.Type}".PadRight(32, ' ') + "|" +
          "Ocupação:".PadRight(34 - Formatter.Boolean(room.isOcupedid).Length, ' ') +
          Formatter.Boolean(room.isOcupedid)
        );
      }

      Console.Write("\nPressione Enter...");
      Console.ReadKey();
    }

    public void ShowDetailsRoom(Room room)
    {
      Console.WriteLine("".PadRight(100, '_'));
      Console.WriteLine(
          "Nº:".PadRight(49 - room.RoomNumber.ToString().Length, ' ') + room.RoomNumber + "|" +
          "Tipo:".PadRight(50 - room.Type.ToString().Length, ' ') + room.Type
        );
      Console.WriteLine(
        $"Ocupação: {Formatter.Boolean(room.isOcupedid)}");
      Console.WriteLine($"Descrição: {room.Description}");
    }

    public void CreateReservation()
    {
      string numberRoom = "";
      string dateCheckIn = "";
      string dateCheckOut = "";

      ShowListRooms();
      Console.WriteLine("");
      Console.Write("\n > Digite o Nº do quarto: ");
      try { numberRoom = Console.ReadLine(); } catch { }

      var room = _hotel.FindRoom(numberRoom);

      if (room == null || room.isOcupedid)
      {
        Console.WriteLine("");
        Console.WriteLine("Nº de quarto indisponível");
        Console.Write("\nPressione Enter...");
        Console.ReadKey();
        return;
      }

      Console.Clear();
      Console.WriteLine("");

      var client = findClient();

      if (client == null)
      {
        Console.Clear();
        Console.WriteLine("");
        Console.WriteLine("Cliente não possui cadastro. Vamos iniciar o registro agora.");
        client = RegisterClient();
      }

      Console.Write("\n > Digite a data de Check-In (DD-MM-YYYY): ");
      try { dateCheckIn = Console.ReadLine(); } catch { }

      Console.Write("\n > Digite a data de Check-Out (DD-MM-YYYY): ");
      try { dateCheckOut = Console.ReadLine(); } catch { }

      var reservation = new Reservation(
        client,
        Formatter.StringToDate(dateCheckIn),
        Formatter.StringToDate(dateCheckOut),
        room
      );

      ShowDetailsReservation(reservation);

      Console.Write("\n > Confirma a reserva? (S/N): ");
      string confirmarPedido = "";
      try
      {
        confirmarPedido = Console.ReadLine().ToUpper();
      }
      catch { }

      if (confirmarPedido == "S")
      {
        reservation.Room.isOcupedid = true;
        reservation.Status = Status.Open;
        client.Reservations.Add(reservation);
        _hotel.Reservations.Add(reservation);
        Persistence.GetInstance.Save();
      }

      Console.WriteLine("");
      Console.WriteLine($"Reserva {reservation.Id} realizada com sucesso!");
      Console.Write("\nPressione Enter...");
      Console.ReadKey();
      return;
    }

    public void FindReservation()
    {
      var reservation = findReservation();

      if (reservation == null)
      {
        Console.WriteLine("");
        Console.WriteLine("Reserva não localizada");
        Console.Write("\nPressione Enter...");
        Console.ReadKey();
        return;
      }

      ShowDetailsReservation(reservation);

      Console.Write("\nPressione Enter...");
      Console.ReadKey();
    }

    private Reservation findReservation()
    {
      if (_hotel.Reservations.Count == 0)
      {
        Console.WriteLine("Não existem reservas cadastradas!");
        Console.Write("\nPressione Enter...");
        Console.ReadKey();
        return null;
      }

      string cod = "";

      Console.Write("\n > Digite o código da reserva ou o número do quarto: ");
      try { cod = Console.ReadLine(); } catch { }

      return _hotel.FindReservation(cod);
    }

    private void ShowDetailsReservation(Reservation reservation)
    {
      Console.WriteLine("");
      Console.WriteLine("".PadRight(100, '_'));
      if (reservation.Status != Status.Pending)
      {
        Console.WriteLine(
          $"Nº da reserva: {reservation.Id}".PadRight(50, ' ') +
          $"Estado da reserva: {reservation.Status}".PadLeft(50, ' ')
        );
      }
      Console.WriteLine($"Data de Check-In: {Formatter.Date(reservation.CheckIn)}");
      Console.WriteLine($"Data de Check-Out: {Formatter.Date(reservation.CheckOut)}");
      Console.WriteLine($"Quantidade de dias: {reservation.TotalDays()}");

      Console.WriteLine(
        $"Valor da diária: {(Price.getValue(reservation.Type)).ToString("C")}".PadRight(50, ' ') +
        $"Valor total: {(Price.getValue(reservation.Type) * reservation.TotalDays()).ToString("C")}".PadLeft(50, ' ')
      );

      ShowDetailsRoom(reservation.Room);
    }
  }
}