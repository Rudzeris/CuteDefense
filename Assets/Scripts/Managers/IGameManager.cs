namespace Assets.Scripts.Managers{
    public interface IGameManager
    {
        StatusManager Status { get; }
        void Startup();
        void Shutdown();
    }
}