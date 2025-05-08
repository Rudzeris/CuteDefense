namespace Assets.Scripts.GameObjects
{
    public interface IController
    {
        void Startup();
        void Shutdown();
    }
    public interface IMoveController : IController { 
        void ReverseDirection();
    }
    public interface IAttackController : IController { }
}
