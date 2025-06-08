using Assets.Scripts.GameObjects.Moving;
using UnityEngine;
namespace Assets.Scripts.GameObjects.Attacks
{
    [RequireComponent(typeof(BasicAttack))]
    [RequireComponent(typeof(AMovable))]
    public class StopMovingOnViewEnemy : MonoBehaviour
    {
        private BasicAttack basicAttack;
        private AMovable amovable;
        void Start()
        {
            basicAttack = GetComponent<BasicAttack>();
            amovable = GetComponent<AMovable>();

            basicAttack.OnViewEnemy += (t) =>
            {
                if (t) amovable.Shutdown();
                else if(!amovable.IsMove) amovable.Startup();
            };
        }
    }
}
