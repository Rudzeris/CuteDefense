using UnityEngine;

namespace Assets.Scripts.GameObjects.Fractions
{
    public class AllyFraction : MonoBehaviour, IFraction
    {
        public FractionType FractionType => FractionType.Ally;
    }
}
