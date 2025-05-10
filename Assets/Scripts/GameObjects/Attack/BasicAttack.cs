using Assets.Scripts.GameObjects.Fractions;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.GameObjects
{
    [RequireComponent(typeof(IFraction))]
    public class BasicAttack : MonoBehaviour, IAttackController
    {
        public event Action OnAttacking;
        public event Action<bool> OnViewEnemy;
        [Header("BasicAttack Parameters")]
        [SerializeField] private int _damage = 3;
        [SerializeField] private float _distanceAttack = 1f;
        [Header("Cooldown Parameters")]
        [SerializeField] private float _cooldown = 1.5f;
        [SerializeField] private float _forAllFirstAttackCooldown = 0.4f;
        private float _currentCooldown = 0;
        protected IFraction Fraction { get; private set; }
        private IBasicEntity _enemyEntity;
        public IBasicEntity EnemyEntity
        {
            get => _enemyEntity;
            protected set { _enemyEntity = value; }
        }
        public bool IsAttack { get; private set; } = true;

        public int Damage => _damage;
        public float DistanceAttack => _distanceAttack;
        public float Cooldown => _cooldown;
        public float ForAllFirstAttackCooldown => _forAllFirstAttackCooldown;

        private void Awake()
        {
            Fraction = GetComponent<IFraction>();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + _distanceAttack * Mathf.Sign(transform.localScale.x)
                , transform.position.y, transform.position.z));
        }
        private IEnumerator Attacking()
        {
            if (_enemyEntity == null)
            {
                FindEnemyEntity();
                yield return new WaitForSeconds(_forAllFirstAttackCooldown);
            }
            if (_enemyEntity != null)
            {
                Attack();
                yield return new WaitForSeconds(Cooldown);
            }
            else
                yield return null;
        }
        private void FindEnemyEntity()
        {
            if (EnemyEntity != null) return;

            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position,
                Mathf.Sign(transform.localScale.x) == -1 ? Vector2.left : Vector2.right,
                _distanceAttack);

            IBasicEntity entity = null;
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null && hit.collider.GetComponent<IFraction>()?.Fraction != this.Fraction.Fraction)
                {
                    entity = hit.collider.GetComponent<IBasicEntity>();
                    if (entity != null && CheckEntity(entity))
                    {
                        break;
                    }
                    else entity = null;
                }
            }
            OnViewEnemy?.Invoke(entity != null);

            EnemyEntity = entity;
            if (EnemyEntity != null)
                EnemyEntity.OnDestroyed += (_) => { EnemyEntity = null; };
        }
        // Проверка BasicEntity - атакуем ли его или нет
        protected virtual bool CheckEntity(IBasicEntity entity)
        {
            return entity is IBasicEntity;
        }
        private void Attack()
        {
            _enemyEntity?.TakeDamage(this.Damage);
            OnAttacking?.Invoke();
            _enemyEntity = null;
        }

        public void Startup() => IsAttack = true;
        public void Shutdown() => IsAttack = false;
    }
}
