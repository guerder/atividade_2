using System;
using System.Collections.Generic;

namespace atividade_2.models
{
  [Serializable]
  public class Client : Entity
  {
    public Client(string name, string dateOfBirth, string rg, string phone, string address, string neighborhood, string city, string state)
    {
      Name = name.ToUpper();
      Rg = rg;
      Phone = phone;
      Address = address;
      Neighborhood = neighborhood;
      City = city;
      State = state;
      Reservations = new List<Reservation>();

      string[] dateInParts = dateOfBirth.Split("-");
      var formattedDateOfBirth = new DateTime(int.Parse(dateInParts[2]), int.Parse(dateInParts[1]), int.Parse(dateInParts[0]));
      DateOfBirth = formattedDateOfBirth;
    }

    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Rg { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string Neighborhood { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public List<Reservation> Reservations { get; set; }
  }
}