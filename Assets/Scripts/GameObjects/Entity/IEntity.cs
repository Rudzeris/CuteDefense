using System;

namespace Assets.Scripts.GameObjects
{
    public interface IEntity
    {
        event Action OnTakenDamage;
        event Action OnDestroyed;
        int HP { get; }
        void TakeDamage(int damage);
    }
    public interface IGameEntity : IEntity { }
    public interface IBase : IEntity { }
}
