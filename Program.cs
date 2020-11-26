using System;
using System.Collections.Generic;
using atividade_2.models;
using atividade_2.services;

namespace atividade_2
{
  class Program
  {
    private static BaseService baseService = new BaseService();
    static void Main(string[] args)
    {
      var menus = new List<Menu>(){
        new Menu(1, "Cadastro", 0),
        new Menu(2, "Check-In", 0),
        new Menu(2, "Check-Out", 0),
        new Menu(3, "Tabela de preços", 0),
        new Menu(4, "Relatório diário", 0),
        new Menu(5, "Cliente", 1),
        new Menu(6, "Reserva", 1),
        new Menu(17, "", 0),
      };
      BuilderMenu builder = new BuilderMenu(menus);

      do
      {
        var idMenu = builder.Build();
        switch (idMenu)
        {
          case 5:
            baseService.FindClient();
            break;

          default:
            Console.WriteLine("\n Opção não implementada.");
            Console.Write("\nPressione Enter...");
            Console.ReadKey();
            break;
        }
      } while (true);
    }
  }
}
