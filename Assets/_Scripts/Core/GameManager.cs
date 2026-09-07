using System;
using RF.Control;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RF.Core
{
    [DefaultExecutionOrder(-9999)]
    public class GameManager : MonoBehaviour
    {
        // SINGLETIN
        public static GameManager Instance;


        // REFS
        public InputManager InputManager { get; set; }
        public PlayerController PlayerController { get; set; }
        public ObstaclePooler ObstaclePooler { get; set; }
        public ObstacleSpawner ObstacleSpawner { get; set; }
        public WorldSpeedManager WorldSpeedManager { get; set; }


        // GAME STATE
        [SerializeField] private GameState state;
        public GameState State => state;

        public event Action<GameState> OnStateChanged;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Debug.LogWarning("Multiple GameManager Instances!");
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void Start()
        {
            InputManager.onJumpStarted += StartGameWithInput;
            InputManager.onCrawlStarted += StartGameWithInput;
        }

        private void OnDisable()
        {
            InputManager.onJumpStarted -= StartGameWithInput;
            InputManager.onCrawlStarted -= StartGameWithInput;
        }

        private void StartGameWithInput()
        {
            if (State == GameState.WaitingToStart)
            {
                SetRunning();
            }

            else if (State == GameState.GameOver)
            {
                SetWaitingToStart();
            }
        }

        public void SetWaitingToStart()
        {
            SetState(GameState.WaitingToStart);
            SceneManager.LoadScene(0);
        }

        public void SetRunning()
        {
            SetState(GameState.Running);
        }

        public void SetGameOver()
        {
            SetState(GameState.GameOver);
        }

        public void SetState(GameState newState, bool forceReset = false)
        {
            if (newState == state && !forceReset) return;

            state = newState;

            OnStateChanged?.Invoke(state);
        }
    }

    public enum GameState { WaitingToStart, Running, GameOver }
}
