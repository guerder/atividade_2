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
      Services = new List<Service>();
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

    public void DoCheckOut()
    {
      Status = Status.Close;
      Room.isOcupedid = false;
    }
  }
}