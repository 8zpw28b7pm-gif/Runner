using UnityEngine;

namespace RF.Obstacles
{
    [CreateAssetMenu(menuName = "New Obstacle")]
    public class ObstacleSO : ScriptableObject
    {
        [SerializeField] private GameObject prefab;

        [SerializeField] private float spawnHeightMin;
        [SerializeField] private float spawnHeightMax;
        [SerializeField] private bool hasVariableSpawnHeight;

        public Obstacle Spawn()
        {
            return Instantiate(prefab).GetComponent<Obstacle>();
        }

        public bool HasVariableSpawnHeight(out float min, out float max)
        {
            if (hasVariableSpawnHeight)
            {
                min = spawnHeightMin;
                max = spawnHeightMax;
                return true;
            }

            min = -1;
            max = -1;
            return false;
        }
    }
}