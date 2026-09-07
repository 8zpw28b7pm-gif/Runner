using System;
using System.Data.Common;
using NUnit.Framework;
using RF.Core;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RF.Control
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float jumpSpeed = 20f;
        [SerializeField] private float jumpCancelMultiplier = 5f;

        [SerializeField] private float jumpingThreshold = 0f;
        [SerializeField] private float fallingThreshold = 0f;


        [Header("HIT BOXES")]
        [SerializeField] private GameObject hitboxRunning;
        [SerializeField] private GameObject hitboxCrawling;


        // REFS
        [SerializeField] private InputManager input;
        private Rigidbody2D body;
        private GroundCheck groundCheck;

        private bool controlsEnabled;
        private bool isDead;
        private bool isCrawling;

        public event Action onDeath;


        private void Awake()
        {
            input = GetComponent<InputManager>();
            body = GetComponent<Rigidbody2D>();
            groundCheck = GetComponentInChildren<GroundCheck>();

            if (input == null)
            {
                input = FindAnyObjectByType<InputManager>();
            }

            GameManager.Instance.PlayerController = this;
        }

        private void OnEnable()
        {
            input.onJumpStarted += Jump;
            input.onJumpCanceled += CancelJump;

            // input.onJumpStarted += StartGameWithInput;
            // input.onCrawlStarted += StartGameWithInput;
        }

        private void OnDisable()
        {
            input.onJumpStarted -= Jump;
            input.onJumpCanceled -= CancelJump;

            // input.onJumpStarted -= StartGameWithInput;
            // input.onCrawlStarted -= StartGameWithInput;
        }

        private void Update()
        {
            if (IsGrounded() && input.IsCrawlPressed)
            {
                Crawl();
            }
            else
            {
                CancelCrawl();
            }
        }

        public bool IsGrounded()
        {
            return groundCheck.isGrounded;
        }

        public bool IsJumping()
        {
            return body.linearVelocityY > jumpingThreshold;
        }

        public bool IsFalling()
        {
            return body.linearVelocityY < fallingThreshold;
        }

        public bool IsCrawling()
        {
            return isCrawling;
        }

        private void Jump()
        {
            if (!IsGrounded()) return;

            body.linearVelocityY = jumpSpeed;
        }

        private void CancelJump()
        {
            if (IsFalling() || IsGrounded()) return;

            body.linearVelocityY /= jumpCancelMultiplier;
        }

        private void Crawl()
        {
            if (!IsGrounded()) return;

            isCrawling = true;
        }

        private void CancelCrawl()
        {
            isCrawling = false;
        }

        public void Die()
        {
            if (isDead) return;

            controlsEnabled = true;

            Collider2D[] colliders = GetComponents<Collider2D>();

            foreach (var col in colliders)
            {
                col.enabled = false;
            }

            body.linearVelocity = Vector2.zero;
            body.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
            body.AddForce(new Vector2(-1, 1) * 8, ForceMode2D.Impulse);

            isDead = true;
            onDeath?.Invoke();

            GameManager.Instance.SetState(GameState.GameOver);
        }

    }
}
