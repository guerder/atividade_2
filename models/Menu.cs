using System;
using System.Collections.Generic;

namespace atividade_2.models
{
  public class Menu : MenuListing
  {
    private bool IsRoot;
    public Menu(string name, bool isRoot = false)
    {
      this.Name = name; this.IsRoot = isRoot;
    }
    public List<MenuListing> includedMenuListings = new List<MenuListing>();
    public void Add(MenuListing obj)
    {
      includedMenuListings.Add(obj);
    }
    public void Remove(MenuListing obj)
    {
      includedMenuListings.Remove(obj);
    }
    public override void Open()
    {
      Titulo(Name);
      for (int i = 0; i < includedMenuListings.Count; i++)
      {
        var includedMenuListingsObject = includedMenuListings[i];
        Console.WriteLine($"{i + 1}. {includedMenuListingsObject.Name}");
      }

      if (!IsRoot)
      {
        Console.WriteLine("\n0. Voltar para o início");
      }

      int option = 0;
      Console.Write("\n > Digite o número correspondente: ");
      try
      {
        option = int.Parse(Console.ReadLine());
      }
      catch
      {
        option = -1;
      }

      if (option == 0 && !IsRoot)
      {
        return;
      }

      if ((option - 1) < 0 || (option) > includedMenuListings.Count)
      {
        Console.WriteLine("\n Opção inválida! Pressione Enter");
        Console.ReadKey();
        return;
      }
      var item = includedMenuListings[option - 1];

      item.Open();
    }
  }
}