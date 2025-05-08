namespace Assets.Scripts.GameObjects.Attack
{
    public class BaseAttacking : EntityAttacking
    {
        protected override bool CheckEntity(IEntity entity) => entity is IBase;
    }
}
