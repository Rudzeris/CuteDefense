using Assets.Scripts.Managers;
using Assets.Scripts.Unit;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class UIUnitButton : MonoBehaviour
    {
        [SerializeField] public AlliedType UnitType;
        [SerializeField] public uint Cost = 10;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(()=>
                LevelManagers.UI.Select(this));
        }
    }
}
