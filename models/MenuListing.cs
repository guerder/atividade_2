using System;

namespace atividade_2.models
{
  public abstract class MenuListing
  {
    public abstract void Open();
    public string Name { get; set; }

    public void Titulo(string titulo)
    {
      Console.Clear();
      titulo = "".PadRight((100 - titulo.Length) / 2, ' ') + titulo.ToUpper();
      Console.WriteLine("".PadRight(100, ' '));
      Console.WriteLine(titulo);
      Console.WriteLine("".PadRight(100, ' '));
    }
  }
}