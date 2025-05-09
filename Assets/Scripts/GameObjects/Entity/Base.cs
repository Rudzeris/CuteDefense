using Assets.Scripts.Managers;

namespace Assets.Scripts.GameObjects
{
    public class Base : BasicEntity, IBase
    {
        private void Awake()
        {
            LevelManager.BaseManager.AddBase(this);
        }
    }
}
