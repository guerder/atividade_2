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
        new Menu(2, "Check-Out", 0),
        new Menu(3, "Tabela de preços", 0),
        new Menu(4, "Relatório diário", 0),
        new Menu(5, "Cliente", 1),
        new Menu(6, "Reserva", 1),
        new Menu(7, "Quartos", 1),
        new Menu(8, "Buscar Cliente", 5),
        new Menu(9, "Cadastrar Cliente", 5),
        new Menu(10, "Listar Clientes", 5),
        new Menu(11, "Buscar Reserva", 6),
        new Menu(12, "Cadastrar Reserva", 6),
        new Menu(13, "Adicionar Serviço", 6),
        new Menu(14, "Listar Quartos", 7),
        new Menu(17, "", 0),
      };
      BuilderMenu builder = new BuilderMenu(menus);

      do
      {
        var idMenu = builder.Build();
        switch (idMenu)
        {
          case 8:
            baseService.FindClient();
            break;
          case 9:
            baseService.RegisterClient();
            break;
          case 10:
            baseService.ShowClients();
            break;
          case 11:
            baseService.FindReservation();
            break;
          case 12:
            baseService.CreateReservation();
            break;
          case 13:
            baseService.AddService();
            break;
          case 14:
            baseService.ShowListRooms();
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
