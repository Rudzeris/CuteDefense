using UnityEngine;

namespace Assets.Scripts.Managers
{
    [RequireComponent(typeof(StateManager))]
    [RequireComponent (typeof(UIController))]
    [RequireComponent (typeof(AlliedManager))]
    public class LevelManagers : BaseManager
    {
        public static StateManager State { get; private set; }
        public static UIController UI { get; private set; }
        public static AlliedManager Allien { get; private set; }

        private void Awake()
        {
            State = GetComponent<StateManager>();
            UI = GetComponent<UIController>();
            Allien = GetComponent<AlliedManager>();

            managers.Add(State);
            managers.Add(UI);
            managers.Add(Allien);
        }
    }
}
