using RF.Control;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource sfxSource;

    [SerializeField] private AudioClip[] footstepsSFX;
    [SerializeField] private AudioClip jumpSFX;
    [SerializeField] private AudioClip deathSFX;

    PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    private void OnEnable()
    {
        playerController.onDeath += PlayDeathSound;
    }

    private void OnDisable()
    {
        playerController.onDeath -= PlayDeathSound;
    }

    // CALLED BY ANIMATOR
    private void PlayFootstepSound()
    {
        AudioClip clipToPlay = footstepsSFX[Random.Range(0, footstepsSFX.Length)];

        AudioSource.PlayClipAtPoint(clipToPlay, Camera.main.transform.position, 1f);
    }

    // CALLED BY ANIMATOR
    private void PlayJumpSound()
    {
        sfxSource.PlayOneShot(jumpSFX, 1f);
    }

    private void PlayDeathSound()
    {
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, 1f);
    }

}
