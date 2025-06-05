using UnityEngine;

namespace Assets.Scripts.GameObjects.Attacks
{
    public class DistancePointsAttack : DistanceAttack
    {
        [SerializeField] private GameObject[] attackPoints;
        private int indexPoint = 0;
        protected override void OnDrawGizmos()
        {
            if (attackPoints.Length > 0)
            {
                Gizmos.color = Color.green;
                foreach (var attackPoint in attackPoints)
                    Gizmos.DrawLine(attackPoint.transform.position,
                        new Vector3(attackPoint.transform.position.x + CurrentDistance * Mathf.Sign(transform.localScale.x),
                        attackPoint.transform.position.y, transform.position.z));
            }
        }
        protected override Vector3 AttackPoint => attackPoints[indexPoint = (indexPoint+1)%attackPoints.Length].transform.position;
    }
}
