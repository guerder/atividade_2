using System;

namespace atividade_2.models
{
  [Serializable]
  public abstract class Entity
  {
    protected Entity()
    {
      Id = String.Concat(Guid.NewGuid().ToString().Split("-"));
    }

    public string Id { get; private set; }
  }
}