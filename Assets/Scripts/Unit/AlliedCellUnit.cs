using System;
using UnityEngine;

namespace Assets.Scripts.Unit
{
    public class AlliedCellUnit : MonoBehaviour, IUnit
    {
        public event Action OnTakenDamage;
        public event Action OnDestroyed;

        public void Shutdown()
        {
            OnDestroyed?.Invoke();
        }

        public void Startup()
        {
            Debug.Log("AllienUnit creating");
        }

        public void TakeDamage()
        {
            OnTakenDamage?.Invoke();
        }
    }
}
