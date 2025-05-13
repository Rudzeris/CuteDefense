using Assets.Scripts.GameObjects.Fractions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Managers
{
    public class GameManager : MonoBehaviour, IManager
    {
        public EStatusManager Status { get; private set; }

        public void Shutdown()
        {
            Status = EStatusManager.Shutdown;
        }

        public void Startup()
        {
            LevelManager.BaseManager.OnEndingGame += EndGame;
            Status = EStatusManager.Started;
        }

        public void EndGame(FractionType type)
        {
            Debug.Log($"Win: {type}");
            ReloadScene();
        }

        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
