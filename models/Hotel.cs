using System;
using System.Collections.Generic;
using System.Linq;
using atividade_2.models.enums;
using atividade_2.utils;

namespace atividade_2.models
{
  [Serializable]
  public class Hotel
  {
    public List<Client> Clients { get; set; }
    public List<Room> Rooms { get; set; }
    public List<Reservation> Reservations { get; set; }

    public Hotel()
    {
      this.Clients = new List<Client>();
      this.Rooms = loadRooms();
      this.Reservations = new List<Reservation>();
    }

    private List<Room> loadRooms()
    {
      var list = new List<Room>();

      var description = "Ambiente único com banheiro(box e espelho), Telefone, Armário, Cofre, \n" +
      "           TV 14', ar-condicionado, Frigobar (sem produtos). \n" +
      "           Não há TV a cabo, apenas a TV aberta(convencional). \n" +
      "           Os apartamentos não possuem ponto de acesso à Internet. \n" +
      "           Não são permitidos animais de estimação nas dependências do Hotel. \n";

      list.Add(new Room(101, false, RoomTypeEnum.Single, description));
      list.Add(new Room(102, false, RoomTypeEnum.Single, description));
      list.Add(new Room(103, false, RoomTypeEnum.Single, description));
      list.Add(new Room(104, false, RoomTypeEnum.Single, description));
      list.Add(new Room(105, false, RoomTypeEnum.Single, description));
      list.Add(new Room(106, false, RoomTypeEnum.Double, description));
      list.Add(new Room(107, false, RoomTypeEnum.Double, description));
      list.Add(new Room(108, false, RoomTypeEnum.Double, description));
      list.Add(new Room(109, false, RoomTypeEnum.Triple, description));
      list.Add(new Room(110, false, RoomTypeEnum.Triple, description));
      list.Add(new Room(201, false, RoomTypeEnum.Single, description));
      list.Add(new Room(202, false, RoomTypeEnum.Single, description));
      list.Add(new Room(203, false, RoomTypeEnum.Single, description));
      list.Add(new Room(204, false, RoomTypeEnum.Single, description));
      list.Add(new Room(205, false, RoomTypeEnum.Single, description));
      list.Add(new Room(206, false, RoomTypeEnum.Double, description));
      list.Add(new Room(207, false, RoomTypeEnum.Double, description));
      list.Add(new Room(208, false, RoomTypeEnum.Double, description));
      list.Add(new Room(209, false, RoomTypeEnum.Triple, description));
      list.Add(new Room(210, false, RoomTypeEnum.Triple, description));
      return list;
    }

    public Client FindClient(string name, string dateOfBirth)
    {
      Client client = null;
      var formattedDateOfBirth = Formatter.StringToDate(dateOfBirth);
      client = Clients.FirstOrDefault(x => x.Name.Equals(name.ToUpper()) && x.DateOfBirth.Equals(formattedDateOfBirth));

      return client;
    }

    public Room FindRoom(string numberRoom)
    {
      return Rooms.FirstOrDefault(x => x.RoomNumber == int.Parse(numberRoom));
    }

    public Reservation FindReservation(string cod)
    {
      return Reservations.FirstOrDefault(x => x.Id.Equals(cod) || x.Room.RoomNumber.ToString().Equals(cod) && x.Room.isOcupedid);
    }
  }
}