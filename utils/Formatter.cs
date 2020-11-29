using System;

namespace atividade_2.utils
{
  public class Formatter
  {
    static public string Date(DateTime? date)
    {
      if (!date.HasValue)
      {
        return "";
      }
      return date.Value.ToString("dd/MM/yyyy");
    }

    static public string Boolean(bool value)
    {
      if (value)
      {
        return "RESERVADO";
      }
      return "LIVRE";
    }

    static public DateTime StringToDate(string date)
    {
      DateTime formattedDate = new DateTime();
      try
      {
        string[] dateInParts = date.Split("-");
        formattedDate = new DateTime(int.Parse(dateInParts[2]), int.Parse(dateInParts[1]), int.Parse(dateInParts[0]));
      }
      catch { }
      return formattedDate;
    }
  }
}