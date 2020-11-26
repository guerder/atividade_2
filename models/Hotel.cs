using System.Collections.Generic;

namespace atividade_2.models
{
  public class Hotel
  {
    public List<Client> Clients { get; set; }
    public List<Room> Rooms { get; set; }
    public List<Reservation> Reservations { get; set; }

    public Hotel()
    {
      this.Clients = new List<Client>();
      this.Rooms = new List<Room>();
      this.Reservations = new List<Reservation>();
    }
  }
}