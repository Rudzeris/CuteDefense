using Assets.Scripts.GameObjects;
using Assets.Scripts.Level;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    [Serializable]
    public struct TypeObject<TKey, TValue>
    {
        public TKey Key;
        public TValue Value;
    }
    public class AllyManager : MonoBehaviour, IManager
    {
        public List<TypeObject<AllyType, GameObject>> AllyObjects = new List<TypeObject<AllyType, GameObject>>();
        public LayerMask TileMask;
        public event Action OnSpawned;
        public EStatusManager Status { get; private set; }

        public void Shutdown()
        {
            Status = EStatusManager.Shutdown;
        }

        public void Startup()
        {
            Status = EStatusManager.Started;
            StartCoroutine(GameUpdate());
        }

        private IEnumerator GameUpdate()
        {
            while (Status == EStatusManager.Started)
            {
                RaycastHit2D hit = Physics2D.Raycast(
                    Camera.main.ScreenToWorldPoint(Input.mousePosition),
                    Vector2.zero,
                    Mathf.Infinity,
                    TileMask
                );
                if (hit.collider?.GetComponent<Cell>() is Cell cell && cell.IsEmpty && LevelManager.UIManager.IsSelect && AllyObjects.Count > 0)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        var pair = AllyObjects.Find(match => match.Key == LevelManager.UIManager.Unit.Type);
                        GameObject gObject = pair.Key != AllyType.None ? pair.Value : null;
                        int cost = LevelManager.UIManager.Unit.Cost;
                        if (gObject == null)
                        {
                            Debug.LogError("AlliedCellManager: AlliedUnits");
                        }
                        else
                        {
                            if (LevelManager.StateManager.IsEnergy(cost))
                            {
                                var obj = Instantiate(gObject,
                                    hit.collider.transform.position,
                                Quaternion.identity
                                );
                                if (obj != null)
                                {
                                    Debug.Log($"AllyEntity Instantiate");
                                    LevelManager.StateManager.ChangeEnergy(-cost);
                                    obj.transform.parent = null;
                                    /*if (obj.GetComponent<IBasicEntity>() is IBasicEntity ucontrol)
                                        cell.AddObject(ucontrol);*/
                                    foreach (var i in obj.GetComponents<IController>())
                                        i.Startup();
                                    OnSpawned?.Invoke();
                                }
                            }
                        }
                    }
                }
                yield return null;
            }
        }
    }
}
