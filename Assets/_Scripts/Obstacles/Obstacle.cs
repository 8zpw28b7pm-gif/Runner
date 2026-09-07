using RF.Core;
using UnityEngine;

namespace RF.Obstacles
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private ObstacleSO obstacleSO;

        private const float minPositionX = -15;

        public ObstacleSO GetObstacleSO()
        {
            return obstacleSO;
        }

        private void Update()
        {
            if (transform.position.x < minPositionX)
            {
                GameManager.Instance.ObstaclePooler.ReturnToPool(this);
            }
        }
    }
}