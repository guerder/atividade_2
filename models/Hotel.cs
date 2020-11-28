using System;
using System.Collections.Generic;
using System.Linq;

namespace atividade_2.models
{
  [Serializable]
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

    public Client FindClient(string name, string dateOfBirth)
    {
      Client client = null;
      try
      {
        string[] dateInParts = dateOfBirth.Split("-");
        var formattedDateOfBirth = new DateTime(int.Parse(dateInParts[2]), int.Parse(dateInParts[1]), int.Parse(dateInParts[0]));
        client = Clients.FirstOrDefault(x => x.Name.Equals(name.ToUpper()) && x.DateOfBirth.Equals(formattedDateOfBirth));
      }
      catch
      { }

      return client;
    }
  }
}