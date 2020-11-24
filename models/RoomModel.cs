using atividade_2.models.enums;

namespace atividade_2.models
{
  public class RoomModel
  {
    public int RoomNumber { get; set; }
    public bool isOcupedid { get; set; }
    public RoomTypeEnum Type { get; set; }
    public string Description { get; set; }
  }
}