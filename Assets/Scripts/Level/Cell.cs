using Assets.Scripts.GameObjects;
using System;
using UnityEngine;

namespace Assets.Scripts.Level
{
    public class Cell : MonoBehaviour
    {
        public bool IsEmpty { get; private set; } = true;
        private IEntity target;
        public void AddObject(IEntity obj)
        {
            if (!IsEmpty)
            {
                Debug.LogWarning($"Cell is fill");
                return;
            }
            if (obj == null)
                throw new ArgumentNullException("obj is null");
            Select(obj);
            target.OnDestroyed += Unselect;
        }
        private void Select(IEntity unitControl)
        {
            IsEmpty = false;
            target = unitControl;
        }
        private void Unselect()
        {
            IsEmpty = true;
            target = null;
        }
        public void Clear()
        {
            if (target != null)
                target.TakeDamage(target.HP);
            Unselect();
        }
    }
}
