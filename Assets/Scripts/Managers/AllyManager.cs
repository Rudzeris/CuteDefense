using Assets.Scripts.GameObjects;
using Assets.Scripts.Level;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Managers
{
    public enum PlacementType { OnCell, Anywhere }
    [Serializable]
    public struct TypeObject<TKey, TValue>
    {
        public TKey Key;
        public TValue Value;
        public PlacementType PlacementType;
    }
    public class AllyManager : MonoBehaviour, IManager
    {
        public List<TypeObject<AllyType, GameObject>> PlacementObjects = new List<TypeObject<AllyType, GameObject>>();
        public LayerMask CellTileMask;
        public event Action OnSpawned;
        public EStatusManager Status { get; private set; }
        private (TypeObject<AllyType, GameObject> TypeObject, int Cost) selectObject;

        public void Shutdown()
        {
            LevelManager.UIManager.OnSelected -= OnSelectObject;
            Status = EStatusManager.Shutdown;
        }

        public void Startup()
        {
            LevelManager.UIManager.OnSelected += OnSelectObject;
            selectObject.Cost = 0;
            selectObject.TypeObject.Key = AllyType.None;
            Status = EStatusManager.Started;
            StartCoroutine(GameUpdate());
        }

        private void OnSelectObject((AllyType type, int cost) obj)
        {
            selectObject = (PlacementObjects.Find(match => match.Key == obj.type), obj.cost);
        }

        private IEnumerator GameUpdate()
        {
            while (Status == EStatusManager.Started)
            {
                /*RaycastHit2D hit = Physics2D.Raycast(
                    Camera.main.ScreenToWorldPoint(Input.mousePosition),
                    Vector2.zero,
                    Mathf.Infinity,
                    CellTileMask
                );
                if (hit.collider?.GetComponent<Cell>() is Cell cell && cell.IsEmpty && LevelManager.UIManager.IsSelect && PlacementObjects.Count > 0)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        var pair = PlacementObjects.Find(match => match.Key == LevelManager.UIManager.Unit.Type);
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
                                    //if (obj.GetComponent<IBasicEntity>() is IBasicEntity ucontrol)
                                        //cell.AddObject(ucontrol);
                                    foreach (var i in obj.GetComponents<IController>())
                                        i.Startup();
                                    OnSpawned?.Invoke();
                                }
                            }
                        }
                    }
                }*/
                if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && LevelManager.StateManager.IsEnergy(selectObject.Cost) && selectObject.TypeObject.Key != AllyType.None)
                {
                    bool spawn = true;
                    Vector3 clickPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Cell cell = null;
                    if (selectObject.TypeObject.PlacementType == PlacementType.OnCell)
                    {
                        spawn = false;
                        RaycastHit2D[] hits = Physics2D.RaycastAll(
                                clickPoint,
                                Vector2.zero,
                                Mathf.Infinity,
                                CellTileMask
                            );
                        foreach (var hit in hits)
                        {
                            cell = hit.collider?.GetComponent<Cell>();
                            if (cell != null)
                            {
                                spawn = true;
                                clickPoint = cell.transform.position;
                                break;
                            }
                        }
                    }
                    else
                        clickPoint = new Vector3(clickPoint.x, clickPoint.y, clickPoint.y / 100);
                    if (spawn)
                    {
                        // Появление
                        var obj = Instantiate(selectObject.TypeObject.Value, clickPoint, Quaternion.identity);
                        if (obj != null)
                        {
                            obj.transform.parent = null;
                            LevelManager.StateManager.ChangeEnergy(-selectObject.Cost);
                            OnSpawned?.Invoke();
                            if (cell != null && GetComponent<IBasicEntity>() is IBasicEntity entity)
                            {
                                cell.AddObject(entity);
                            }
                            foreach (var i in obj.GetComponents<IController>())
                                i.Startup();
                        }
                    }
                }

                yield return null;
            }
        }


    }
}
