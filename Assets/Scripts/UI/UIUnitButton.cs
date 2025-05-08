using Assets.Scripts.Managers;
using Assets.Scripts.GameObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class UIUnitButton : MonoBehaviour
    {
        [SerializeField] public AlliedType AlliedType;
        [SerializeField] public uint Cost = 10;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(Select);
        }
        public void Select() => LevelManagers.UIManager.Select(this);
        public void UnSelect()
        {

        }
    }
}
