using System;
using UnityEngine;

namespace Assets.Scripts.Unit
{
    public class UnitBase : MonoBehaviour, IUnitControl
    {
        public event Action OnTakenDamage;
        public event Action OnDestroyed;
        [SerializeField] private uint _hp = 1;
        [SerializeField] private BaseType _base;
        public uint HP => _hp;

        public BaseType Base { get => _base; set => _base = value; }

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
