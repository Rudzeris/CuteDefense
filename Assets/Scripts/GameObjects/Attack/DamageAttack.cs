using Assets.Scripts.GameObjects.Fractions;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.GameObjects
{
    public class DamageAttack : BasicAttack
    {
        public override event Action OnAttacking;
        public override event Action<bool> OnViewEnemy;

        [SerializeField] private int damage;
        public int Damage => damage;

        protected override IEnumerator Attack()
        {
            GameObject enemy = null;
            while (enemy == null)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, DistanceAttack, gameObject.layer);

                if (hit.collider != null && hit.collider?.GetComponent<IFraction>()?.Fraction != this.Fraction.Fraction)
                {
                    OnViewEnemy?.Invoke(true);
                    if (hit.collider?.GetComponent<IBasicEntity>() is IBasicEntity entity && CheckEntity(entity))
                    {
                        entity.TakeDamage(Damage);
                        OnAttacking?.Invoke();
                    }
                }

                yield return null;
            }
        }
    }
}
