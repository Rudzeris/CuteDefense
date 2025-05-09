using UnityEngine;

namespace Assets.Scripts.Managers
{
    [RequireComponent(typeof(StateManager))]
    [RequireComponent(typeof(UIController))]
    [RequireComponent(typeof(AlliedCellManager))]
    [RequireComponent(typeof(BaseManager))]
    public class LevelManager : BasicManager
    {
        public static StateManager StateManager { get; private set; }
        public static UIController UIManager { get; private set; }
        public static AlliedCellManager AlliedManager { get; private set; }

        public static BaseManager BaseManager { get; private set; }

        private void Awake()
        {
            StateManager = GetComponent<StateManager>();
            UIManager = GetComponent<UIController>();
            AlliedManager = GetComponent<AlliedCellManager>();
            BaseManager = GetComponent<BaseManager>();

            managers.Add(StateManager);
            managers.Add(UIManager);
            managers.Add(AlliedManager);
            managers.Add(BaseManager);
        }
    }
}
