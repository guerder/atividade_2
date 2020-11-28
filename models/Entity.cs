using System;

namespace atividade_2.models
{
  [Serializable]
  public abstract class Entity
  {
    protected Entity()
    {
      Id = Guid.NewGuid();
    }

    public Guid Id { get; private set; }
  }
}