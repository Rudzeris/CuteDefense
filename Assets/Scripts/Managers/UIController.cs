using Assets.Scripts.UI;
using Assets.Scripts.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class UIController : MonoBehaviour, IGameManager
    {
        public EStatusManager Status { get; private set; }
        private UIUnitButton UIButton;
        public (AlliedType Type, uint Cost) Unit => (UIButton?.AlliedType??AlliedType.None, UIButton?.Cost??0);
        public bool IsSelect => UIButton != null;
        public void Shutdown()
        {
            LevelManagers.AlliedManager.OnSpawned -= UnSelect;
            Status = EStatusManager.Shutdown;
        }

        public void Startup()
        {
            LevelManagers.AlliedManager.OnSpawned += UnSelect;
            Status = EStatusManager.Started;
        }
        private void UnSelect() => UIButton = null;
        public void Select(UIUnitButton button)
        {
            Debug.Log($"Button: Select {button.AlliedType}");
            UIButton?.UnSelect();
            UIButton = UIButton == button ? null : button;
        }
    }
}
