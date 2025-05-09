using System;

namespace Assets.Scripts.GameObjects
{
    public interface IBasicEntity
    {
        event Action<IBasicEntity,int> OnTakenDamage;
        event Action<IBasicEntity> OnDestroyed;
        int HP { get; }
        void TakeDamage(int damage);
    }
    public interface IEntity : IBasicEntity { }
    public interface IBase : IBasicEntity { }
}
