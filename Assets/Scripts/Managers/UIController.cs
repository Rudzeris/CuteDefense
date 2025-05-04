using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class UIController : MonoBehaviour, IGameManager
    {
        public EStatusManager Status { get; private set; }
        public UIUnitButton UIButton { get; private set; }
        public void Shutdown()
        {
            Status = EStatusManager.Shutdown;
        }

        public void Startup()
        {
            Status = EStatusManager.Started;
        }
        public void Select(UIUnitButton button)
        {
            Debug.Log($"Button: Select {button.UnitType}");
            UIButton?.UnSelect();
            UIButton = UIButton == button ? null : button;
        }
    }
}
