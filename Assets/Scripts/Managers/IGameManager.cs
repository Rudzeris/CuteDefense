namespace Assets.Scripts.Managers{
    public interface IGameManager
    {
        EStatusManager Status { get; }
        void Startup();
        void Shutdown();
    }
}