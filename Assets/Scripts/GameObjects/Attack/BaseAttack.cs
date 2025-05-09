namespace Assets.Scripts.GameObjects.Attack
{
    public class BaseAttack : BasicAttack
    {
        protected override bool CheckEntity(IBasicEntity entity) => entity is IBase;
    }
}
