using Assets.Scripts.GameObjects.Fractions;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.GameObjects
{
    [RequireComponent(typeof(IFraction))]
    public class Entity : MonoBehaviour, IEntity
    {
        public event Action OnTakenDamage;
        public event Action OnDestroyed;
        [SerializeField] private int _hp = 1;
        private IFraction _fraction;
        public bool IsDestroyed { get; private set; }
        public int HP => _hp;

        public void TakeDamage(int damage)
        {
            _hp = Math.Clamp(_hp - damage, 0, _hp);
            OnTakenDamage?.Invoke();
            if (_hp == 0)
            {
                StartCoroutine(Destroyed());
            }
        }

        private IEnumerator Destroyed()
        {
            OnDestroyed?.Invoke();
            if (!IsDestroyed)
            {
                IsDestroyed = true;
                yield return null;
                Destroy(this.gameObject);
            }
        }
    }
}
