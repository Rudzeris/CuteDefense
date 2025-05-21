using Assets.Scripts.GameObjects.Entities;
using Assets.Scripts.GameObjects.Fractions;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.GameObjects.Attacks
{
    public class DamageAttack : BasicAttack
    {
        public override event Action OnAttacking;
        public override event Action<bool> OnViewEnemy;

        [SerializeField] private int damage;
        public int Damage => damage;

        protected override IEnumerator Attack()
        {
            bool attacking = false;
            while (!attacking && IsActive)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, DistanceAttack);

                if (hit.collider != null && (hit.collider?.GetComponent<IFraction>()?.Fraction ?? this.Fraction.Fraction) != this.Fraction.Fraction)
                {
                    if (hit.collider?.GetComponent<IBasicEntity>() is IBasicEntity entity && CheckEntity(entity))
                    {
                        OnViewEnemy?.Invoke(true);
                        entity.TakeDamage(Damage);
                        OnAttacking?.Invoke();
                        attacking = true;
                    }
                }

                yield return null;
            }
        }
    }
}
