using Assets.Scripts.GameObjects.Attacks;
using Assets.Scripts.GameObjects.Fractions;
using UnityEngine;

namespace Assets.Scripts.GameObjects
{
    [RequireComponent(typeof(IFraction))]
    public class UnitController : MonoBehaviour, IMoveController
    {
        [SerializeField] Direction2 direction;
        [SerializeField] private float _maxSpeed = 2f;
        [SerializeField] private float _acceleration = 1f;
        [SerializeField] private float _deceleration = 10f;
        [SerializeField] private float _currentSpeed = 0;

        public bool IsMove { get; protected set; } = true;
        public float CurrentSpeed => _currentSpeed;
        public float MaxSpeed => _maxSpeed;
        public float Acceleration => _acceleration;
        public float Deceleration => _deceleration;
        public Direction2 Direction
        {
            get { return direction; }
            private set
            {
                direction = value;
            }
        }
        private void Start()
        {
            if (GetComponent<BasicAttack>() is BasicAttack attack)
            {
                attack.OnViewEnemy += (v) => IsMove = !v;
            }
        }
        private void Update()
        {
            transform.localScale = new Vector3(
                Direction switch { Direction2.Left => -1, _ => 1 } * Mathf.Abs(transform.localScale.x),
                transform.localScale.y, transform.localScale.z);
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y/100f);
        }
        private void FixedUpdate()
        {
            _currentSpeed = (IsMove
                ? (_currentSpeed >= _maxSpeed ? _maxSpeed : _currentSpeed + _acceleration * Time.fixedDeltaTime)
                : (_currentSpeed <= 0 ? 0 : _currentSpeed - _deceleration * Time.fixedDeltaTime));
            Move();
        }

        protected virtual void Move()
        {
            transform.Translate(
               new Vector2[3] {
                    Vector2.zero,Vector2.left, Vector2.right
               }[(int)direction]
               * _currentSpeed * Time.fixedDeltaTime);
        }
        public void Shutdown()
        {
            IsMove = false;
        }

        public void Startup()
        {
            IsMove = true;
        }

        public void ReverseDirection()
        {
            this.direction = this.direction switch
            {
                Direction2.Left => Direction2.Right,
                Direction2.Right => Direction2.Left,
                _ => Direction2.None
            };
        }
    }
}