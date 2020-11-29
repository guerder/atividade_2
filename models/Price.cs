using atividade_2.models.enums;

namespace atividade_2.models
{
  public class Price
  {
    public static double SINGLE_PRICE = 80.0;
    public static double DOUBLE_PRICE = 120.0;
    public static double TRIPLE_PRICE = 150.0;
    public static double TELEPHONE = 10.0;
    public static double FOOD = 40.0;
    public static double CONVENIENCE_FEE = 0.05;

    public static double getValue(RoomTypeEnum type)
    {
      return type == RoomTypeEnum.Single ? SINGLE_PRICE :
             type == RoomTypeEnum.Double ? DOUBLE_PRICE :
             TRIPLE_PRICE;
    }
  }
}