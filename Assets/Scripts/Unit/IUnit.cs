using System;

namespace Assets.Scripts.Unit
{
    public interface IUnit
    {
        event Action OnTakenDamage;
        event Action OnDestroyed;

        void Startup();
        void TakeDamage();
        void Shutdown();
    }
}
