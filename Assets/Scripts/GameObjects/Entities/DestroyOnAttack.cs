using Assets.Scripts.GameObjects.Attacks;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.GameObjects.Entities

{
    [RequireComponent(typeof(BasicAttack))]
    public class DestroyOnAttack : MonoBehaviour, IBasicEntity
    {
        BasicAttack attack;
        private bool isDestroy = false;

        public int HP => 0;

        public int MaxHP => 0;

        public event Action<IBasicEntity, int> OnTakenDamage;

        public event Action<IBasicEntity> OnDestroyed;

        private void Start()
        {
            attack = GetComponent<BasicAttack>();
            attack.OnViewEnemy += (e) => { if (e) attack.Shutdown(); };
            attack.OnAttacking += () =>
            {
                OnTakenDamage?.Invoke(this, 1);
                StartCoroutine(Destroyed());
            };
        }
        private IEnumerator Destroyed()
        {
            if (!isDestroy)
            {
                isDestroy = true;
                yield return null;
                /*while (attack.IsAttack)
                {
                    yield return null;
                }*/
                OnDestroyed?.Invoke(this);
                Destroy(gameObject);
            }
        }

        public void TakeDamage(int damage)
        {
            throw new NotImplementedException();
        }
    }
}
