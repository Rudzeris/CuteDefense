namespace Assets.Scripts.GameObjects.Attack
{
    public class EntityTarget : Target
    {
        public override bool CheckEntity(IBasicEntity entity)
        {
            return entity is IEntity;
        }
    }
}
