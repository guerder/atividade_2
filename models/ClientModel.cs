using System.Collections.Generic;

namespace atividade_2.models
{
  public class ClientModel
  {
    public string Name { get; set; }
    public string Rg { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string Neighborhood { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public List<ReservationModel> Reservations { get; set; }
  }
}