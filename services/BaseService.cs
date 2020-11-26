using System;
using atividade_2.Data;
using atividade_2.models;

namespace atividade_2.services
{
  public class BaseService
  {
    private static Hotel _hotel = Persistence.GetInstance.GetBaseHotel();

    public void FindClient()
    {
      string name = "";
      string dateOfBirth = "";

      Console.Write("\n > Digite o nome do cliente: ");
      try
      {
        name = Console.ReadLine();
      }
      catch { }

      Console.Write("\n > Digite a data de nascimento (DD-MM-YYYY): ");
      try
      {
        dateOfBirth = Console.ReadLine();
      }
      catch { }

      // TODO: Exibir resultado
    }
  }
}