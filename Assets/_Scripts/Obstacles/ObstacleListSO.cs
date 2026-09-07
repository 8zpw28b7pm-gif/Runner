using UnityEngine;

namespace RF.Obstacles
{
    [CreateAssetMenu(menuName = "New Obstacle List")]
    public class ObstacleListSO : ScriptableObject
    {
        [SerializeField] private ObstacleSO[] obstacleList;

        public ObstacleSO[] GetObstacleList()
        {
            return obstacleList;
        }
    }
}