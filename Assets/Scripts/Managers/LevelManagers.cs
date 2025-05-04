using UnityEngine;

namespace Assets.Scripts.Managers
{
    [RequireComponent(typeof(StateManager))]
    [RequireComponent (typeof(UIController))]
    [RequireComponent (typeof(AlliedCellManager))]
    public class LevelManagers : BaseManager
    {
        public static StateManager State { get; private set; }
        public static UIController UI { get; private set; }
        public static AlliedCellManager Allien { get; private set; }

        private void Awake()
        {
            State = GetComponent<StateManager>();
            UI = GetComponent<UIController>();
            Allien = GetComponent<AlliedCellManager>();

            managers.Add(State);
            managers.Add(UI);
            managers.Add(Allien);
        }
    }
}
