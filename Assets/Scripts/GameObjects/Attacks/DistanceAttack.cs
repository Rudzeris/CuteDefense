using Assets.Scripts.GameObjects.Entities;
using Assets.Scripts.GameObjects.Fractions;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.GameObjects.Attacks
{
    public class DistanceAttack : AttackWithTime
    {
        public override event Action OnAttacking;
        public override event Action<bool> OnViewEnemy;
        public Vector3 BulletSpawnPoint;
        public GameObject Bullet;
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position + BulletSpawnPoint, new Vector3(transform.position.x + DistanceAttack * Mathf.Sign(transform.localScale.x)
                , transform.position.y, transform.position.z));
        }
        protected override IEnumerator Attack()
        {
            // Ищет противника в одной линии и атакует
            IBasicEntity enemyEntity = null;
            while (IsActive)
            {
                if (enemyEntity == null)
                {
                    RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Mathf.Sign(transform.localScale.x) == -1 ? Vector2.left : Vector2.right, DistanceAttack);

                    enemyEntity = null;
                    foreach (RaycastHit2D hit in hits)
                    {
                        if (hit.collider != null && (hit.collider.GetComponent<IFraction>()?.Fraction ?? this.Fraction.Fraction) != this.Fraction.Fraction)
                        {
                            enemyEntity = hit.collider.GetComponent<IBasicEntity>();
                            if (enemyEntity != null && CheckEntity(enemyEntity))
                            {
                                break;
                            }
                            else enemyEntity = null;
                        }
                    }
                    OnViewEnemy?.Invoke(enemyEntity != null);

                    if (enemyEntity != null) // Если спереди челик, то 1-я атака ждет
                        yield return new WaitForSeconds(ForAllFirstAttackCooldown);
                }
                if (enemyEntity != null)
                {
                    GameObject obj = Instantiate(Bullet, transform.position + BulletSpawnPoint, Quaternion.identity, this.gameObject.transform);
                    if (obj != null)
                    {
                        OnAttacking?.Invoke();
                        enemyEntity = null;

                        // TODO: Настроить Bullet

                        yield return new WaitForSeconds(Cooldown);
                    }
                    else
                        yield return null;
                }
                else
                    yield return null;
            }
        }
    }
}
