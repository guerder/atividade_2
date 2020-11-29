using System;

namespace atividade_2.models
{
  [Serializable]
  public class Payment : Entity
  {
    private Random random = new Random();

    public Payment(double total)
    {
      Total = total;
      TotalPaid = GeneratePaymentValue(total);
    }

    public double Total { get; set; }
    public double TotalPaid { get; set; }

    private double GeneratePaymentValue(double total)
    {
      var factor = random.Next(0, 3);
      return factor == 0 ? 0 : factor == 1 ? total : random.Next(10, Convert.ToInt32(total));
    }
  }
}