using System;

namespace atividade_2.models
{
  [Serializable]
  public class Service : Entity
  {
    public Service(DateTime date)
    {
      Date = date;
      Phone = false;
      Food = false;
    }

    public DateTime Date { get; set; }
    public bool Phone { get; set; }
    public bool Food { get; set; }
  }
}