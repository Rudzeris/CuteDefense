namespace Assets.Scripts.GameObjects.Attack
{
    public class EntityAttack : BasicAttack
    {
        protected override bool CheckEntity(IBasicEntity entity) => entity is IEntity;
    }
}
