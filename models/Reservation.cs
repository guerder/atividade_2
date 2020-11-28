using System;
using System.Collections.Generic;
using atividade_2.models.enums;

namespace atividade_2.models
{
  [Serializable]
  public class Reservation : Entity
  {
    public Client Client { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public Room Room { get; set; }
    public RoomTypeEnum type { get; set; }
    public List<Service> Services { get; set; }
    public Payment Payment { get; set; }
  }
}