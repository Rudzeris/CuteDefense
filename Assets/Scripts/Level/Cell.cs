using Assets.Scripts.Unit;
using System;
using UnityEngine;

namespace Assets.Scripts.Level
{
    public class Cell : MonoBehaviour
    {
        public bool IsEmpty { get; private set; }
        private AlliedCellUnit target;
        public void AddObject(AlliedCellUnit obj)
        {
            if (!IsEmpty)
            {
                Debug.LogWarning($"Cell is fill");
                return;
            }
            if (obj == null)
                throw new ArgumentNullException("obj is null");
            IsEmpty = false;
            target = obj;
            target.Startup();
        }
        public void Clear()
        {
            IsEmpty = true;
            if (target != null)
                target.Shutdown();
        }
    }
}
