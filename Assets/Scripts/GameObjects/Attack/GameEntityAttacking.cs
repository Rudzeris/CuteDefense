namespace Assets.Scripts.GameObjects.Attack
{
    public class GameEntityAttacking : EntityAttacking
    {
        protected override bool CheckEntity(IEntity entity) => entity is IGameEntity;
    }
}
