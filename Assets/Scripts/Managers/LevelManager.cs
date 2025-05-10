using UnityEngine;

namespace Assets.Scripts.Managers
{
    [RequireComponent(typeof(StateManager))]
    [RequireComponent(typeof(UIController))]
    [RequireComponent(typeof(AllyManager))]
    [RequireComponent(typeof(BaseManager))]
    public class LevelManager : BasicManager
    {
        public static StateManager StateManager { get; private set; }
        public static UIController UIManager { get; private set; }
        public static AllyManager AllyManager { get; private set; }
        public static BaseManager BaseManager { get; private set; }

        private void Awake()
        {
            StateManager = GetComponent<StateManager>();
            UIManager = GetComponent<UIController>();
            AllyManager = GetComponent<AllyManager>();
            BaseManager = GetComponent<BaseManager>();

            managers.Add(StateManager);
            managers.Add(UIManager);
            managers.Add(AllyManager);
            managers.Add(BaseManager);
        }
    }
}
