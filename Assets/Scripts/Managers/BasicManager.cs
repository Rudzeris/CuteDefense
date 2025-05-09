using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public abstract class BasicManager : MonoBehaviour
    {
        protected List<IGameManager> managers = new List<IGameManager>();
        protected virtual void Start()
        {
            StartGame();
        }
        public void StartGame() => StartCoroutine(StartupManagers());
        public void EndGame() => StartCoroutine(ShutdownManagers());
        protected virtual IEnumerator StartupManagers()
        {
            foreach (IGameManager manager in managers)
                manager.Startup();
            yield return null;
            for (int i = 0, last = 0; i < managers.Count; i = 0)
            {
                foreach (IGameManager manager in managers)
                    if (manager.Status == EStatusManager.Started) i++;
                if (i > last)
                {
                    last = i;
                    Debug.Log($"BasicManager: {last}/{managers.Count}");
                }
                yield return null;
            }
        }
        protected virtual IEnumerator ShutdownManagers()
        {
            foreach (IGameManager manager in managers)
                manager.Startup();
            yield return null;
            for (int i = 0, last = 0; i < managers.Count; i = 0)
            {
                foreach (IGameManager manager in managers)
                    if (manager.Status == EStatusManager.Started) i++;
                if (i > last)
                {
                    last = i;
                    Debug.Log($"BasicManager: {last}/{managers.Count}");
                }
                yield return null;
            }
        }
    }
}