namespace Assets.Scripts.GameObjects.Attack
{
    public class BaseTarget : Target
    {
        public override bool CheckEntity(IBasicEntity entity)
        {
            return entity is IBase;
        }
    }
}
