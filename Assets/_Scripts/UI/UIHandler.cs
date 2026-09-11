using System;
using RF.Core;
using UnityEngine;

namespace RF.UI
{
    [DefaultExecutionOrder(-8888)]
    public class UIHandler : MonoBehaviour
    {
        public static UIHandler Instance;

        [SerializeField] private GameObject startPrompt;
        [SerializeField] private GameObject gameOverPrompt;

        public Fader Fader { get; set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Debug.LogWarning("Multiple UIHandler Instances!");
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void OnEnable()
        {
            GameManager.Instance.onStateChanged += GameManager_OnStateChanged;
        }

        private void OnDisable()
        {
            GameManager.Instance.onStateChanged -= GameManager_OnStateChanged;
        }

        private void GameManager_OnStateChanged(GameState state)
        {
            if (state == GameState.WaitingToStart)
            {
                ShowStartPrompt(true);
                ShowGameOverPrompt(false);
            }
            else if (state == GameState.GameOver)
            {
                ShowStartPrompt(false);
                ShowGameOverPrompt(true);
            }
            else
            {
                ShowStartPrompt(false);
                ShowGameOverPrompt(false);
            }
        }

        private void ShowStartPrompt(bool show)
        {
            startPrompt.SetActive(show);
        }

        private void ShowGameOverPrompt(bool show)
        {
            gameOverPrompt.SetActive(show);
        }
    }
}
