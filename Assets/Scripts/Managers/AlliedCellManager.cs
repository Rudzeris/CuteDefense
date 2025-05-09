using Assets.Scripts.Level;
using Assets.Scripts.GameObjects;
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
    public class AlliedCellManager : MonoBehaviour, IManager
    {
        public List<TypeObject<AlliedType, GameObject>> AlliedUnits = new List<TypeObject<AlliedType, GameObject>>();
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
                if (hit.collider?.GetComponent<Cell>() is Cell cell && cell.IsEmpty && LevelManager.UIManager.IsSelect && AlliedUnits.Count > 0)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        var pair = AlliedUnits.Find(match => match.Key == LevelManager.UIManager.Unit.Type);
                        GameObject gObject = pair.Key != AlliedType.None ? pair.Value : null;

                        if (gObject == null)
                        {
                            Debug.LogError("AlliedCellManager: AlliedUnits");
                        }
                        else
                        {
                            var obj = Instantiate(gObject,
                                hit.collider.transform.position,
                            Quaternion.identity
                            );
                            if (obj != null)
                            {
                                Debug.Log($"AllyEntity Instantiate");
                                obj.transform.parent = null;
                                if (obj.GetComponent<IBasicEntity>() is IBasicEntity ucontrol)
                                    cell.AddObject(ucontrol);
                                OnSpawned?.Invoke();
                            }
                        }
                    }
                }
                yield return null;
            }
        }
    }
}
