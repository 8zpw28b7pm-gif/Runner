using System;
using RF.Core;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace RF.UI
{
    public class ScoreDisplay : MonoBehaviour
    {
        [SerializeField] private ScoreManager scoreManager;

        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI highScoreText;

        private void Awake()
        {
            if (scoreManager == null)
            {
                scoreManager = FindAnyObjectByType<ScoreManager>();
            }
        }

        private void OnEnable()
        {
            scoreManager.onHighScoreChanged += UpdateHighScoreText;
        }

        private void OnDisable()
        {
            scoreManager.onHighScoreChanged -= UpdateHighScoreText;
        }

        private void Start()
        {
            UpdateHighScoreText();
        }

        private void Update()
        {
            int score = GameManager.Instance.ScoreManager.GetScore();
            scoreText.text = string.Format("{0:00000}", score);
        }

        private void UpdateHighScoreText()
        {
            int highScore = ScoreManager.highScore;
            highScoreText.text = "HI " + string.Format("{0:00000}", highScore);
        }
    }
}
