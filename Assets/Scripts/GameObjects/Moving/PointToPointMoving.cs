using System.Collections;
using UnityEngine;

namespace Assets.Scripts.GameObjects.Moving
{
    public class PointToPointMoving : MonoBehaviour, IController
    {
        [Header("Movement Settings")]
        public GameObject fromObject;         // Откуда летит
        public float speed = 5f;              // Скорость падения
        public float destroyDelay = 0f;       // Задержка перед уничтожением
        private BasicAttack controller;
        private bool isMoving = false;
        private Vector3 targetPoint;           // Куда приземлиться
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
                yield return null;
            }

            transform.position = targetPoint;

            if (destroyDelay > 0f)
                yield return new WaitForSeconds(destroyDelay);
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
            transform.position = fromObject.transform.position;
            if (!isMoving)
                StartCoroutine(MoveDownward());
        }

        public void Shutdown()
        {
            isMoving = false;
            Destroy(gameObject);
        }
    }
}
