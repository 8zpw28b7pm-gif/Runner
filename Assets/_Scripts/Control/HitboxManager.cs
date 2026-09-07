using UnityEngine;

namespace RF.Control
{
    public class HitboxManager : MonoBehaviour
    {
        [SerializeField] private Hitbox hitboxRun;
        [SerializeField] private Hitbox hitboxCrawl;

        private PlayerController playerController;

        private void Awake()
        {
            playerController = GetComponentInParent<PlayerController>();
        }

        private void OnEnable()
        {
            hitboxRun.onHit += playerController.Die;
            hitboxCrawl.onHit += playerController.Die;
        }
        
        private void OnDisable()
        {
            hitboxRun.onHit -= playerController.Die;
            hitboxCrawl.onHit -= playerController.Die;
        }

        private void Update()
        {
            if (playerController.IsCrawling())
            {
                hitboxRun.gameObject.SetActive(false);
                hitboxCrawl.gameObject.SetActive(true);
            }
            else
            {
                hitboxCrawl.gameObject.SetActive(false);
                hitboxRun.gameObject.SetActive(true);
            }
        }
    }
}