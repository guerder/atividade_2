using System;

namespace atividade_2.models
{
  [Serializable]
  public class Payment : Entity
  {
    public double Total { get; set; }
    public double TotalPaid { get; set; }
  }
}