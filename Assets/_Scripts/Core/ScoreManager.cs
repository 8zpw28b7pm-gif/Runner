using System;
using UnityEngine;

namespace RF.Core
{
    public class ScoreManager : MonoBehaviour
    {
        public static int highScore;
        
        [SerializeField] private int score;
        
        private float timeSinceLastPoint = Mathf.Infinity;
        public event Action onHighScoreChanged;

        private void Awake()
        {
            GameManager.Instance.ScoreManager = this;
        }

        private void OnEnable()
        {
            GameManager.Instance.onStateChanged += GameManager_OnStateChanged;
        }

        private void GameManager_OnStateChanged(GameState state)
        {
            if (state == GameState.GameOver)
            {
                if (score > highScore)
                {
                    highScore = score;
                    onHighScoreChanged?.Invoke();
                }
            }
        }

        private void Update()
        {
            if (GameManager.Instance.State != GameState.Running) return;

            timeSinceLastPoint += Time.deltaTime;

            float pointInterval = 0.1f;

            if (timeSinceLastPoint > pointInterval)
            {
                timeSinceLastPoint = 0;
                score++;
            }
        }

        public void AwardPoints(int points)
        {
            if (points <= 0) return;

            score += points;
        }

        public int GetScore()
        {
            return score;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetStatics()
        {
            highScore = 0;
        }
    }
}
