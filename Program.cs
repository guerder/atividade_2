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
      Menu main = new Menu("Menu Principal", true);

      Menu register = new Menu("Cadastro");
      ItemSimple checkOut = new ItemSimple("Check-Out", baseService.CheckOut);
      ItemSimple table = new ItemSimple("Tabela de preços", baseService.TablePrices);
      ItemSimple report = new ItemSimple("Relatório diário", baseService.DailyReport);

      main.Add(register);
      main.Add(checkOut);
      main.Add(table);
      main.Add(report);

      Menu client = new Menu("Cliente");
      Menu reservation = new Menu("Reserva");
      Menu room = new Menu("Quarto");
      register.Add(client);
      register.Add(reservation);
      register.Add(room);

      ItemSimple findClient = new ItemSimple("Buscar Cliente", baseService.FindClient);
      ItemSimple registerClient = new ItemSimple("Cadastrar Cliente", baseService.RegisterClient);
      ItemSimple listClients = new ItemSimple("Listar Clientes", baseService.ShowClients);
      client.Add(findClient);
      client.Add(registerClient);
      client.Add(listClients);

      ItemSimple findReservation = new ItemSimple("Buscar Reserva", baseService.FindReservation);
      ItemSimple createReservation = new ItemSimple("Cadastrar Reserva", baseService.CreateReservation);
      ItemSimple addService = new ItemSimple("Adicionar Serviço", baseService.AddService);
      reservation.Add(findReservation);
      reservation.Add(createReservation);
      reservation.Add(addService);

      ItemSimple listRooms = new ItemSimple("Listar Quartos", () => baseService.ShowListRooms());
      room.Add(listRooms);

      do
      {
        main.Open();
      } while (true);
    }
  }
}
