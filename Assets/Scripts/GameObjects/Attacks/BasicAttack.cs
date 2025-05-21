using Assets.Scripts.GameObjects.Entities;
using Assets.Scripts.GameObjects.Fractions;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.GameObjects.Attacks
{
    [RequireComponent(typeof(IFraction))]
    [RequireComponent(typeof(ITarget))]
    public class BasicAttack : MonoBehaviour
    {
        public virtual event Action OnAttacking;
        public virtual event Action<bool> OnViewEnemy;
        [Header("BasicAttack Parameters")]
        [SerializeField] private int _damage = 3;
        [SerializeField] private float _distanceAttack = 1f;
        [Header("Cooldown Parameters")]
        [SerializeField] private float _cooldown = 1.5f;
        [SerializeField] private float _forAllFirstAttackCooldown = 0.4f;
        public bool AutoStart = true;
        protected IFraction Fraction { get; private set; }
        protected ITarget Target { get; private set; }
        public bool IsActive { get; private set; } = false;
        public bool IsAttack { get; private set; } = false;

        public int Damage => _damage;
        public float DistanceAttack => _distanceAttack;
        public float Cooldown => _cooldown;
        public float ForAllFirstAttackCooldown => _forAllFirstAttackCooldown;

        private void Awake()
        {
            Fraction = GetComponent<IFraction>();
            Target = GetComponent<ITarget>();
        }
        private void Start()
        {
            if(AutoStart)
                Startup();
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + _distanceAttack * Mathf.Sign(transform.localScale.x)
                , transform.position.y, transform.position.z));
        }
        protected virtual IEnumerator Attack()
        {
            // Ищет противника в одной линии и атакует
            IsAttack = true;
            IBasicEntity enemyEntity = null;
            while (IsActive)
            {
                if (enemyEntity == null)
                {
                    RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Mathf.Sign(transform.localScale.x) == -1 ? Vector2.left : Vector2.right, _distanceAttack);

                    enemyEntity = null;
                    foreach (RaycastHit2D hit in hits)
                    {
                        if (hit.collider != null && hit.collider.GetComponent<IFraction>()?.Fraction != this.Fraction.Fraction)
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
                        yield return new WaitForSeconds(_forAllFirstAttackCooldown);
                }
                if (enemyEntity != null)
                {
                    enemyEntity?.TakeDamage(this.Damage);
                    OnAttacking?.Invoke();
                    enemyEntity = null;
                    yield return new WaitForSeconds(Cooldown);
                }
                else
                    yield return null;
            }
            IsAttack = false;
        }
        // Проверка BasicEntity - атакуем ли его или нет
        protected bool CheckEntity(IBasicEntity entity)
        {
            return Target.CheckEntity(entity);
        }

        public void Startup()
        {
            if (!IsActive)
            {
                IsActive = true;
                StartCoroutine(Attack());
            }
        }
        public void Shutdown() => IsActive = false;
    }
}
