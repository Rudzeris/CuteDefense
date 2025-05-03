using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public abstract class BaseManager : MonoBehaviour
    {
        protected List<IGameManager> managers;
        protected virtual void Start()
        {
            GameStart();
        }
        public void GameStart() => StartCoroutine(StartupManagers());
        public void GameEnd() => StartCoroutine(ShutdownManagers());
        protected virtual IEnumerator StartupManagers()
        {
            foreach (IGameManager manager in managers)
                manager.Startup();
            yield return null;
            for (int i = 0, last = 0; i < managers.Count; i = 0)
            {
                foreach (IGameManager manager in managers)
                    if (manager.Status == StatusManager.Started) i++;
                if (i > last)
                {
                    last = i;
                    Debug.Log($"BaseManager: {last}/{managers.Count}");
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
                    if (manager.Status == StatusManager.Started) i++;
                if (i > last)
                {
                    last = i;
                    Debug.Log($"BaseManager: {last}/{managers.Count}");
                }
                yield return null;
            }
        }
    }
}