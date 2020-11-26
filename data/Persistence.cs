using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using atividade_2.models;

namespace atividade_2.Data
{
  public class Persistence
  {
    private static Persistence _instance;
    private Hotel _hotel;

    protected Persistence()
    {
      _hotel = new Hotel();
      try
      {
        FileStream fs = new FileStream("data.bin", FileMode.Open);
        BinaryFormatter bf = new BinaryFormatter();
        _hotel = (Hotel)bf.Deserialize(fs);
        fs.Close();
      }
      catch { }
    }

    public static Persistence GetInstance
    {
      get
      {
        if (_instance == null)
        {
          _instance = new Persistence();
        }
        return _instance;
      }
    }

    public Hotel GetBaseHotel()
    {
      return _hotel;
    }
    public void Save()
    {
      try
      {
        FileStream fs = new FileStream("data.bin", FileMode.Create);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(fs, _hotel);
        fs.Close();
      }
      catch (Exception e)
      {
        Console.WriteLine("erro na gravação dos dados: " + e);
        Console.ReadKey();
      }
    }
  }
}