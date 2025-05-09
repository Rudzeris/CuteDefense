using Assets.Scripts.Managers;

namespace Assets.Scripts.GameObjects
{
    public class Base : BasicEntity, IBase
    {
        private void Start()
        {
            LevelManager.BaseManager.AddBase(this);
        }
    }
}
