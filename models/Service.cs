using System;

namespace atividade_2.models
{
  [Serializable]
  public class Service : Entity
  {
    public DateTime Date { get; set; }
    public bool Phone { get; set; }
    public bool Food { get; set; }
  }
}