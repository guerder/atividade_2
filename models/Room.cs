using System;
using atividade_2.models.enums;

namespace atividade_2.models
{
  [Serializable]
  public class Room : Entity
  {
    public int RoomNumber { get; set; }
    public bool isOcupedid { get; set; }
    public RoomTypeEnum Type { get; set; }
    public string Description { get; set; }
  }
}