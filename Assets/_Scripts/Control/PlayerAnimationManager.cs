using RF.Core;
using UnityEngine;

namespace RF.Control
{
    public class PlayerAnimationManager : MonoBehaviour
    {
        [SerializeField] private float worldSpeedFraction = 0f;
        [SerializeField] private float animationSpeedModifier = 0f;

        // REFS
        private PlayerController playerController;
        private Animator animator;

        private const string IDLE_CLIP = "Idle";
        private const string RUN_CLIP = "Run";
        private const string JUMP_CLIP = "Jump";
        private const string FALL_CLIP = "Fall";
        private const string CRAWL_CLIP = "Crawl";


        private void Awake()
        {
            playerController = GetComponent<PlayerController>();
            animator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            HandleAnimationSpeed();

            if (GameManager.Instance.State != GameState.Running)
            {
                PlayClip(IDLE_CLIP);
                return;
            }

            if (playerController.IsJumping())
            {
                PlayClip(JUMP_CLIP);
                return;
            }

            if (playerController.IsFalling())
            {
                PlayClip(FALL_CLIP);
                return;
            }

            if (playerController.IsGrounded())
            {
                if (playerController.IsCrawling())
                {
                    PlayClip(CRAWL_CLIP);
                }
                else
                {
                    PlayClip(RUN_CLIP);
                }
            }
        }

        private void PlayClip(string clipName)
        {
            if (IsPlaying(clipName)) return;

            animator.Play(clipName);
        }

        private bool IsPlaying(string clipName)
        {
            return animator.GetCurrentAnimatorStateInfo(0).IsName(clipName);
        }

        private void HandleAnimationSpeed()
        {
            worldSpeedFraction = GameManager.Instance.WorldSpeedManager.GetSpeedFraction();
            animator.speed = worldSpeedFraction + animationSpeedModifier;
        }
    }
}