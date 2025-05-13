using UnityEngine;

namespace Assets.Scripts.GameObjects.Attack
{
    public class Target : MonoBehaviour, ITarget
    {
        public virtual bool CheckEntity(IBasicEntity entity)
        {
            return entity is IBasicEntity;
        }
    }
}
