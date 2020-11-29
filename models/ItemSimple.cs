using System;

namespace atividade_2.models
{
  public class ItemSimple : MenuListing
  {
    private delegate void ActionItem();
    private ActionItem ActionExecute;
    public ItemSimple(string name, Action function)
    {
      this.Name = name;
      ActionExecute = new ActionItem(function);
    }
    public override void Open()
    {
      Titulo(Name);
      ActionExecute();
    }
  }
}