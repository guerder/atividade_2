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

      Console.Write("\nPressione Enter...");
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

    public void RegisterClient()
    {
      registerClient();
    }
    private Client registerClient()
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
      Console.WriteLine("".PadRight(100, '_'));
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
        client = registerClient();
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
      ShowDetailsRoom(reservation.Room);

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

        // Simula se o valor da reserva foi creditado e o valor creditado.
        reservation.SimulatorPayment();

        Persistence.GetInstance.Save();

        Console.WriteLine("");
        Console.WriteLine($"Reserva {reservation.Id} realizada com sucesso!");
        Console.Write("\nPressione Enter...");
        Console.ReadKey();
        return;
      }
      else
      {
        Console.WriteLine("");
        Console.WriteLine($"Transação não confirmada.");
        Console.Write("\nPressione Enter...");
        Console.ReadKey();
        return;
      }
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
      ShowDetailsRoom(reservation.Room);

      Console.Write("\nPressione Enter...");
      Console.ReadKey();
    }

    public void AddService()
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

      showDetailsService(reservation);

      string dia = "";
      string typeService = "";

      Console.Write("\n > Digite o número correspondente ao DIA: ");
      try { dia = Console.ReadLine(); } catch { }

      Service service = null;
      try
      {
        service = reservation.Services[int.Parse(dia) - 1];
      }
      catch { }

      if (service == null)
      {
        Console.WriteLine("");
        Console.WriteLine("DIA digitado inválido");
        Console.Write("\nPressione Enter...");
        Console.ReadKey();
        return;
      }

      Console.Write("\n > Digite 'A' para Alimentação, e 'T' para Telefone: ");
      try { typeService = Console.ReadLine(); } catch { }

      if (typeService.ToUpper().Equals("A") || typeService.ToUpper().Equals("T"))
      {
        if (typeService.ToUpper().Equals("A"))
        {
          service.Food = true;
          typeService = "Alimentação";
        }
        else
        {
          service.Phone = true;
          typeService = "Telefone";
        }
      }
      else
      {
        Console.WriteLine("");
        Console.WriteLine("Tipo de serviço digitado inválido");
        Console.Write("\nPressione Enter...");
        Console.ReadKey();
      }

      Persistence.GetInstance.Save();
      Console.Clear();
      Console.WriteLine("");
      Console.Write($"Adicionado {typeService} na data {Formatter.Date(service.Date)} com sucesso");
      showDetailsService(reservation);
      Console.Write("\nPressione Enter...");
      Console.ReadKey();
    }

    public void DailyReport()
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

      showDetailsService(reservation);
      Console.Write("\nPressione Enter...");
      Console.ReadKey();
    }

    public void TablePrices()
    {
      Console.WriteLine(" > HOSPEDAGEM");
      Console.WriteLine("");
      Console.WriteLine(
        $"   Simples (Single)".PadRight(50, '.') +
        $"{Price.SINGLE_PRICE.ToString("C")}".PadLeft(50, '.')
      );
      Console.WriteLine(
        $"   Dupla (Double)".PadRight(50, '.') +
        $"{Price.DOUBLE_PRICE.ToString("C")}".PadLeft(50, '.')
      );
      Console.WriteLine(
        $"   Tripla (Triple)".PadRight(50, '.') +
        $"{Price.TRIPLE_PRICE.ToString("C")}".PadLeft(50, '.')
      );
      Console.WriteLine("");

      Console.WriteLine(" > SERVIÇOS");
      Console.WriteLine("");
      Console.WriteLine(
        $"   Alimentação".PadRight(50, '.') +
        $"{Price.FOOD.ToString("C")}".PadLeft(50, '.')
      );
      Console.WriteLine(
        $"   Uso do Telefone".PadRight(50, '.') +
        $"{Price.TELEPHONE.ToString("C")}".PadLeft(50, '.')
      );

      Console.WriteLine("");
      Console.WriteLine("");
      Console.WriteLine("   *OBSERVAÇÃO: OS VALORES ACIMA REPRESENTAM O CUSTO POR DIA.");
      Console.WriteLine("");

      Console.Write("\nPressione Enter...");
      Console.ReadKey();
    }

    public void CheckOut()
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

      ShowDetailsReservation(reservation, true);

      if (reservation.Status == Status.Close)
      {
        Console.WriteLine("");
        Console.WriteLine($"Reserva {reservation.Id} já está finalizada!");
        Console.Write("\nPressione Enter...");
        Console.ReadKey();
        return;
      }

      Console.Write("\n > Confirma o procedimento de Check-Out? (S/N): ");
      string confirmarPedido = "";
      try
      {
        confirmarPedido = Console.ReadLine().ToUpper();
      }
      catch { }

      if (confirmarPedido == "S")
      {
        reservation.Room.isOcupedid = false;
        reservation.Status = Status.Close;
        Console.WriteLine("");
        Console.WriteLine($"Reserva {reservation.Id} finalizada com sucesso!");
        Console.Write("\nPressione Enter...");
        Console.ReadKey();
        return;
      }
      else
      {
        Console.WriteLine("");
        Console.WriteLine($"Transação não confirmada.");
        Console.Write("\nPressione Enter...");
        Console.ReadKey();
        return;
      }
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

    private void ShowDetailsReservation(Reservation reservation, bool isCheckout = false)
    {
      double total = reservation.TotalDaily() + reservation.TotalServices();
      double fee = 0;
      if (isCheckout)
      {
        fee = total * Price.CONVENIENCE_FEE;
        total += fee - reservation.Payment.TotalPaid;
      }

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

      if (isCheckout)
      {
        tableServices(reservation);
      }

      Console.WriteLine(
        $"Diária: {(Price.getValue(reservation.Type)).ToString("C")} X {reservation.TotalDays()} = {reservation.TotalDaily().ToString("C")}".PadLeft(100, ' ')
      );

      if (reservation.Status != Status.Pending)
      {
        Console.WriteLine(
          $"Serviços (Alimentação + Telefone): {reservation.TotalServices().ToString("C")}".PadLeft(100, ' ')
        );
        var wasDepositedEarly = reservation.Payment.TotalPaid != 0 ? "Sim" : "Não";
        Console.Write(
          $"Feito depósito antecipado da reserva? {wasDepositedEarly}".PadRight(50, ' ')
        );
        if (reservation.Payment.TotalPaid != 0)
        {
          Console.WriteLine(
            $"Valor depositado: {reservation.Payment.TotalPaid.ToString("C")}".PadLeft(50, ' ')
          );
        }
      }
      if (isCheckout)
      {
        Console.WriteLine(
          $"Taxa de atendimento: {fee.ToString("C")}".PadLeft(100, ' ')
        );
      }
      Console.WriteLine("");
      Console.WriteLine(
        $"TOTAL: {total.ToString("C")}".PadLeft(100, ' ')
      );
    }

    private void showDetailsService(Reservation reservation)
    {
      Console.WriteLine("");
      Console.WriteLine("".PadRight(100, '_'));
      Console.WriteLine(
        $"Nº da reserva: {reservation.Id}".PadRight(50, ' ') +
        $"Estado da reserva: {reservation.Status}".PadLeft(50, ' ')
      );

      tableServices(reservation);
      Console.WriteLine("");
      Console.WriteLine($"TOTAL: {(reservation.TotalDaily() + reservation.TotalServices()).ToString("C")}".PadLeft(100, ' '));
      Console.WriteLine("".PadRight(100, '_'));
    }

    private void tableServices(Reservation reservation)
    {
      Console.WriteLine("".PadRight(100, '_'));
      Console.WriteLine(
        "Dia" +
        "|Data ".PadRight(20, ' ') +
        "|Hospedagem ".PadRight(20, ' ') +
        "|Alimentação ".PadRight(20, ' ') +
        "|Telefone ".PadRight(20, ' ') +
        "|Sub-total"
      );

      double total = 0;
      for (int index = 0; index < reservation.Services.Count; index++)
      {
        var service = reservation.Services[index];
        var dailyPrice = Price.getValue(reservation.Type);
        var foodPrice = service.Food ? Price.FOOD : 0;
        var phonePrice = service.Phone ? Price.TELEPHONE : 0;
        var sum = dailyPrice + foodPrice + phonePrice;
        total += sum;
        Console.WriteLine(
          $"{(index + 1).ToString().PadLeft(3, ' ')}" +
          $"|{Formatter.Date(service.Date)}".PadRight(20, ' ') +
          $"|{dailyPrice.ToString("C")}".PadRight(20, ' ') +
          $"|{foodPrice.ToString("C")}".PadRight(20, ' ') +
          $"|{phonePrice.ToString("C")}".PadRight(20, ' ') +
          $"|{sum.ToString("C")}"
        );
      }
      Console.WriteLine("".PadRight(100, '_'));
    }
  }
}