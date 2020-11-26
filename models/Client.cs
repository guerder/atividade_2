using System;
using System.Collections.Generic;

namespace atividade_2.models
{
  public class Client : Entity
  {
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