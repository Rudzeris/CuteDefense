using Assets.Scripts.GameObjects.Attacks;
using Assets.Scripts.GameObjects.Entities;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.GameObjects.Moving
{
    public class PointToPointMoving : MonoBehaviour, IController, IBasicEntity
    {
        [Header("Movement Settings")]
        public GameObject fromObject;         // Откуда летит
        public Vector3 fromPoint;         // Откуда летит
        public float speed = 5f;              // Скорость падения
        public float destroyDelay = 0f;       // Задержка перед уничтожением
        private BasicAttack controller;
        private bool isMoving = false;
        private Vector3 targetPoint;           // Куда приземлиться

        public int HP => 0;

        public event Action<IBasicEntity, int> OnTakenDamage;
        public event Action<IBasicEntity> OnDestroyed;

        private void Start()
        {
            controller = GetComponent<BasicAttack>();
            if (controller != null)
            {
                controller.OnAttacking += Shutdown;
            }
        }

        private IEnumerator MoveDownward()
        {
            isMoving = true;
            while (Vector3.Distance(transform.position, targetPoint) > 0.05f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y / 100);
                yield return null;
            }

            transform.position = new Vector3(targetPoint.x, targetPoint.y, targetPoint.y / 100);

            if (destroyDelay > 0f)
                yield return new WaitForSeconds(destroyDelay);
            OnTakenDamage?.Invoke(this, 0);
            controller.Startup();
        }

        public void Startup()
        {
            // Если назначен источник — берем позицию
            if (controller == null)
            {
                controller = GetComponent<BasicAttack>();
                controller.OnAttacking += Shutdown;
            }
            controller?.Shutdown();
            targetPoint = transform.position;
            if (fromObject != null)
                transform.position = fromObject.transform.position;
            else
                transform.position = fromPoint;
            if (!isMoving)
                StartCoroutine(MoveDownward());
        }

        public void Shutdown()
        {
            isMoving = false;
            OnDestroyed?.Invoke(this);
            Destroy(gameObject);
        }

        public void TakeDamage(int damage)
        {
            throw new NotImplementedException();
        }
    }
}
