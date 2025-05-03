using Assets.Scripts.Level;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class AlliedManager : MonoBehaviour, IGameManager
    {
        public LayerMask TileMask;
        public StatusManager Status { get; private set; }

        public void Shutdown()
        {
            Status = StatusManager.Shutdown;
        }

        public void Startup()
        {
            Status = StatusManager.Started;
            StartCoroutine(GameUpdate());
        }

        private IEnumerator GameUpdate()
        {
            while (Status == StatusManager.Started)
            {
                // Нажатие на что-то
                Cell cell = null;
                if (cell != null)
                {
                    RaycastHit2D hit = Physics2D.Raycast(
                        Camera.main.ScreenToWorldPoint(Input.mousePosition),
                        Vector2.zero,
                        Mathf.Infinity,
                        TileMask
                    );
                    if(hit.collider && LevelManagers.UI)
                    {

                    }
                }
                yield return null;
            }
        }
    }
}
