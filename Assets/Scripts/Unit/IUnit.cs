using System;

namespace Assets.Scripts.Unit
{
    public interface IUnit
    {
        event Action OnTakenDamage;
        event Action OnDestroyed;
        BaseType Base { get; }
        uint HP { get; }
        void TakeDamage();
    }
    public interface IControl
    {
        void Startup();
        void Shutdown();
    }
    public interface IUnitControl : IControl, IUnit
    {
    }
}
