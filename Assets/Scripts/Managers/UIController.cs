using Assets.Scripts.GameObjects;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class UIController : MonoBehaviour, IManager
    {
        public EStatusManager Status { get; private set; }
        private UIEntityButton UIButton;
        public (AllyType Type, uint Cost) Unit => (UIButton?.AlliedType ?? AllyType.None, UIButton?.Cost ?? 0);
        public bool IsSelect => UIButton != null;
        public void Shutdown()
        {
            LevelManager.AllyManager.OnSpawned -= UnSelect;
            Status = EStatusManager.Shutdown;
        }

        public void Startup()
        {
            LevelManager.AllyManager.OnSpawned += UnSelect;
            Status = EStatusManager.Started;
        }
        private void UnSelect()
        {
#if !UNITY_EDITOR
            UIButton = null;
#endif
        }
        public void Select(UIEntityButton button)
        {
            UIButton?.UnSelect();
            UIButton = UIButton == button ? null : button;
        }
    }
}
