using System;
using RF.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RF.Control
{
    public class InputManager : MonoBehaviour
    {
        private PlayerControls playerControls;

        public bool Jump { get; private set; }

        public event Action onJumpStarted;
        public event Action onJumpCanceled;

        public event Action onCrawlStarted;
        public event Action onCrawlCanceled;

        public bool IsCrawlPressed { get; private set; }


        private void Awake()
        {
            playerControls = new PlayerControls();
            GameManager.Instance.InputManager = this;
        }

        private void OnEnable()
        {
            playerControls.Player.Jump.started += OnJump;
            playerControls.Player.Jump.canceled += OnJump;

            playerControls.Player.Duck.started += OnCrawl;
            playerControls.Player.Duck.canceled += OnCrawl;

            playerControls.Enable();
        }

        private void OnDisable()
        {
            playerControls.Player.Jump.started -= OnJump;
            playerControls.Player.Jump.canceled -= OnJump;

            playerControls.Player.Duck.started -= OnCrawl;
            playerControls.Player.Duck.canceled -= OnCrawl;

            playerControls.Disable();
        }

        private void OnJump(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                onJumpStarted?.Invoke();
            }
            else if (context.canceled)
            {
                onJumpCanceled?.Invoke();
            }
        }

        private void OnCrawl(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                onCrawlStarted?.Invoke();
                IsCrawlPressed = true;
            }
            else if (context.canceled)
            {
                onCrawlCanceled?.Invoke();
                IsCrawlPressed = false;
            }
        }
    }
}
