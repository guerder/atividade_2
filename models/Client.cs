using System;
using System.Collections.Generic;
using atividade_2.utils;

namespace atividade_2.models
{
  [Serializable]
  public class Client : Entity
  {
    public Client(string name, string dateOfBirth, string rg, string phone, string address, string neighborhood, string city, string state)
    {
      Name = name.ToUpper();
      DateOfBirth = Formatter.StringToDate(dateOfBirth);
      Rg = rg;
      Phone = phone;
      Address = address;
      Neighborhood = neighborhood;
      City = city;
      State = state;
      Reservations = new List<Reservation>();
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