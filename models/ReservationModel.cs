using System;
using System.Collections.Generic;
using atividade_2.models.enums;

namespace atividade_2.models
{
  public class ReservationModel
  {
    public ClientModel Client { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public RoomModel Room { get; set; }
    public RoomTypeEnum type { get; set; }
    public List<ServiceModel> Services { get; set; }
  }
}