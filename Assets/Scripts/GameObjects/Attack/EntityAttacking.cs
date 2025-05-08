using Assets.Scripts.GameObjects.Fractions;
using System;
using UnityEngine;

namespace Assets.Scripts.GameObjects
{
    [RequireComponent(typeof(IFraction))]
    public class EntityAttacking : MonoBehaviour, IAttackController
    {
        public event Action OnAttacking;
        public event Action<bool> OnViewEnemy;
        [Header("EntityAttacking Parameters")]
        [SerializeField] private int _damage = 3;
        [SerializeField] private float _distanceAttack = 1f;
        [Header("Cooldown Parameters")]
        [SerializeField] private float _cooldown = 1.5f;
        [SerializeField] private float _forAllFirstAttackCooldown = 0.4f;
        private float _currentCooldown = 0;
        protected IFraction Fraction { get; private set; }
        private IEntity _enemyEntity;
        public IEntity EnemyEntity
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
        private void Update()
        {
            if (_currentCooldown <= 0 && IsAttack)
            {
                FindEnemyEntity();
                if (_enemyEntity != null)
                {
                    if (_currentCooldown <= -_forAllFirstAttackCooldown)
                        Attack();
                    else
                        _currentCooldown -= Time.deltaTime;
                }
            }
            else
                _currentCooldown -= Time.deltaTime;
        }
        private void FindEnemyEntity()
        {
            if (EnemyEntity != null) return;

            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position,
                Mathf.Sign(transform.localScale.x) == -1 ? Vector2.left : Vector2.right,
                _distanceAttack);

            IEntity entity = null;
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null && hit.collider.GetComponent<IFraction>()?.FractionType != this.Fraction.FractionType)
                {
                    entity = hit.collider.GetComponent<IEntity>();
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
                EnemyEntity.OnDestroyed += () => { EnemyEntity = null; };
        }
        // Проверка Entity - атакуем ли его или нет
        protected virtual bool CheckEntity(IEntity entity)
        {
            return entity is IEntity;
        }
        private void Attack()
        {
            _enemyEntity?.TakeDamage(this.Damage);
            _enemyEntity = null;
        }

        public void Startup() => IsAttack = true;
        public void Shutdown() => IsAttack = false;
    }
}
