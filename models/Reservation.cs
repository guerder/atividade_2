using System;
using System.Collections.Generic;
using atividade_2.models.enums;

namespace atividade_2.models
{
  [Serializable]
  public class Reservation : Entity
  {
    public Reservation(Client client, DateTime checkIn, DateTime checkOut, Room room)
    {
      Client = client;
      CreatedAt = DateTime.Now;
      CheckIn = checkIn;
      CheckOut = checkOut;
      Room = room;
      Type = room.Type;
      Services = GenerateServices();
      Status = Status.Pending;
    }

    public Client Client { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public Room Room { get; set; }
    public RoomTypeEnum Type { get; set; }
    public List<Service> Services { get; set; }
    public Payment Payment { get; set; }
    public Status Status { get; set; }

    public void SimulatorPayment()
    {
      double dailyPrice = Price.getValue(Type);
      Payment = new Payment(TotalDays() * dailyPrice);
    }

    public int TotalDays()
    {
      TimeSpan diff = CheckOut.Subtract(CheckIn);
      return diff.Days;
    }

    public double TotalDaily()
    {
      double total = 0;
      foreach (var service in Services)
      {
        var dailyPrice = Price.getValue(Type);
        total += dailyPrice;
      }
      return total;
    }

    public double TotalServices()
    {
      double total = 0;
      foreach (var service in Services)
      {
        var foodPrice = service.Food ? Price.FOOD : 0;
        var phonePrice = service.Phone ? Price.TELEPHONE : 0;
        total += foodPrice + phonePrice;
      }
      return total;
    }

    public void DoCheckOut()
    {
      Status = Status.Close;
      Room.isOcupedid = false;
    }

    private List<Service> GenerateServices()
    {
      List<Service> list = new List<Service>();
      for (int i = 0; i < TotalDays(); i++)
      {
        var currentDate = CheckIn.AddDays(i);
        list.Add(new Service(currentDate));
      }
      return list;
    }
  }
}