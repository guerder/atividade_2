using System;

namespace atividade_2.models
{
  public abstract class Entity
  {
    protected Entity()
    {
      Id = Guid.NewGuid();
    }

    public Guid Id { get; private set; }
  }
}