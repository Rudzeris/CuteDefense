using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class StateManager : MonoBehaviour, IGameManager
    {
        public StatusManager Status { get; private set; }
        public Progress Progress { get; private set; }

        public void Startup()
        {
            Debug.Log("State manager starting...");
            Debug.Log($"Progress: {Progress}");
            Status = StatusManager.Started;
        }

        public void Shutdown()
        {
            Debug.Log("State manager shutdown...");
            Status = StatusManager.Shutdown;
        }
        public bool IsEnergy(uint energy) => Progress.Energy - energy>= 0;
        public void AddEnergy(uint energy)
            => Progress.Energy +=
            IsEnergy(energy)?
            energy :
            0;
    }
    public class Progress
    {
        public uint Energy { get; set; }
        public override string ToString()
            => '{' + $"Energy: {Energy}" + '}';
    }
}
