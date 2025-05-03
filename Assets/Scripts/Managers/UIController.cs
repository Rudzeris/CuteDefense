using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class UIController : MonoBehaviour, IGameManager
    {
        public StatusManager Status { get; private set; }
        public UIUnitButton UIButton { get; private set; }
        public void Shutdown()
        {
            Status = StatusManager.Shutdown;
        }

        public void Startup()
        {
            Status = StatusManager.Started;
        }
        public void Select(UIUnitButton button)
        {
            UIButton = UIButton == button ? null : button;
        }
    }
}
