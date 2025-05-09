using Assets.Scripts.GameObjects;
using Assets.Scripts.GameObjects.Fractions;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class BaseManager : MonoBehaviour, IManager
    {
        private List<IBase> bases = new List<IBase>();
        private Dictionary<FractionType, int> countBases = new Dictionary<FractionType, int>()
        {
            {FractionType.None,0},
            {FractionType.Ally,0},
            {FractionType.Enemy,0}
        };
        public EStatusManager Status { get; private set; }

        public void Shutdown()
        {
            Status = EStatusManager.Shutdown;
        }

        public void Startup()
        {
            Status = EStatusManager.Initializing;
            foreach (IBase i in bases)
                if (i is MonoBehaviour mono && mono?.GetComponent<IFraction>() is IFraction fraction)
                {
                    switch (fraction.Fraction)
                    {
                        case FractionType.Ally:
                            i.OnDestroyed += AllyBaseDestroyed;
                            break;
                        case FractionType.Enemy:
                            i.OnDestroyed += EnemyBaseDestroyed;
                            break;
                    }
                    countBases[fraction.Fraction] += 1;
                }
            Status = EStatusManager.Started;
        }
        public void AddBase(IBase ibase)
        {
            bases.Add(ibase);
        }
        private void EndGameCheck()
        {
            bool allyDef = false;
            bool enemyDef = false;
            if (countBases[FractionType.Ally] == 0)
                allyDef = true;
            if (countBases[FractionType.Enemy] == 0)
                enemyDef = true;

            if (allyDef && enemyDef)
                Debug.Log("Game: Tie");
            else if (enemyDef)
                Debug.Log("Game: Win");
            else if (allyDef)
                Debug.Log("Game: Defeat");
        }
        private void AllyBaseDestroyed(IBasicEntity entity)
        {
            if (entity is IBase eBase)
            {
                bases.Remove(eBase);
                countBases[FractionType.Ally] -= 1;
                Debug.Log("Ally base destroyed");
                EndGameCheck();
            }
        }
        private void EnemyBaseDestroyed(IBasicEntity entity)
        {
            if (entity is IBase eBase)
            {
                bases.Remove(eBase);
                countBases[FractionType.Enemy] -= 1;
                Debug.Log("Enemy base destroyed");
                EndGameCheck();
            }
        }
    }
}
