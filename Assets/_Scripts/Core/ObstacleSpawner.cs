
using RF.Obstacles;
using UnityEngine;

namespace RF.Core
{
    public class ObstacleSpawner : MonoBehaviour
    {
        [SerializeField] private float spawnTimeMin;
        [SerializeField] private float spawnTimeMax;

        private float timer = Mathf.Infinity;
        private float nextSpawnTime = Mathf.Infinity;

        [SerializeField] private float spawnPositionX;

        [SerializeField] private ObstacleListSO obstacleListSO;

        private void Awake()
        {
            GameManager.Instance.ObstacleSpawner = this;
        }

        private void Start()
        {
            CalculateNextSpawnTime();
        }

        private void Update()
        {
            if (GameManager.Instance.State != GameState.Running) return;

            UpdateTimer();

            if (timer > nextSpawnTime)
            {
                timer = 0;
                Spawn();
                CalculateNextSpawnTime();
            }
        }

        private void UpdateTimer()
        {
            timer += Time.deltaTime;
        }

        private void Spawn()
        {
            int randomIndex = Random.Range(0, obstacleListSO.GetObstacleList().Length);
            ObstacleSO obstacleSO = obstacleListSO.GetObstacleList()[randomIndex];

            Obstacle spawnedObstacle = GameManager.Instance.ObstaclePooler.GrabFromPool(obstacleSO);
            spawnedObstacle.transform.position = GetSpawnPosition(obstacleSO);
        }

        private Vector3 GetSpawnPosition(ObstacleSO obstacleSO)
        {
            float spawnPositionY = 0f;

            if (obstacleSO.HasVariableSpawnHeight(out float minHeight, out float maxHeight))
            {
                spawnPositionY = Random.Range(minHeight, maxHeight);
                spawnPositionY = Mathf.Round(spawnPositionY * 2f) / 2f;
            }

            return new Vector3(spawnPositionX, spawnPositionY, 0);
        }

        private void CalculateNextSpawnTime()
        {
            nextSpawnTime = Random.Range(spawnTimeMin, spawnTimeMax);
        }
    }
}
