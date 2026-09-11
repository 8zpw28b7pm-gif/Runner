
using System.Collections.Generic;
using RF.Obstacles;
using UnityEngine;

namespace RF.Core
{
    public class ObstacleSpawner : MonoBehaviour
    {
        [SerializeField] private float spawnTimeMin;
        [SerializeField] private float spawnTimeMax;

        private float spawnTimer = Mathf.Infinity;
        private float nextSpawnTime = Mathf.Infinity;

        [SerializeField] private float spawnPositionX;

        [SerializeField] private ObstacleListSO obstacleListSO;
        [SerializeField] private List<ObstacleSO> availableObstacles;

        [SerializeField] private float addObstacleInterval;
        private float timeSinceAddedObstacle = Mathf.NegativeInfinity;
        private float addObstacleTimer;


        private void Awake()
        {
            GameManager.Instance.ObstacleSpawner = this;
        }

        private void Start()
        {
            CalculateNextSpawnTime();

            for (int i = 0; i < 2; i++)
            {
                if (obstacleListSO.GetObstacleList().Length <= i)
                {
                    Debug.Log("No more obstacles in list");
                    return;
                }
                availableObstacles.Add(obstacleListSO.GetObstacleList()[i]);
            }
        }

        private void Update()
        {
            if (GameManager.Instance.State != GameState.Running) return;

            UpdateTimers();

            if (spawnTimer > nextSpawnTime)
            {
                spawnTimer = 0;
                Spawn();
                CalculateNextSpawnTime();
            }

            if (addObstacleTimer > addObstacleInterval)
            {
                addObstacleTimer = 0;
                AddObstacle();
            }
        }

        private void AddObstacle()
        {
            ObstacleSO[] obstacleArray = obstacleListSO.GetObstacleList();

            if (obstacleArray.Length > availableObstacles.Count)
            {
                int nextIndex = availableObstacles.Count;
                availableObstacles.Add(obstacleArray[nextIndex]);
            }
        }

        private void UpdateTimers()
        {
            spawnTimer += Time.deltaTime;
            addObstacleTimer += Time.deltaTime;
        }

        // private void Spawn()
        // {
        //     int randomIndex = Random.Range(0, obstacleListSO.GetObstacleList().Length);
        //     ObstacleSO obstacleSO = obstacleListSO.GetObstacleList()[randomIndex];

        //     Obstacle spawnedObstacle = GameManager.Instance.ObstaclePooler.GrabFromPool(obstacleSO);
        //     spawnedObstacle.transform.position = GetSpawnPosition(obstacleSO);
        // }

        private void Spawn()
        {
            int randomIndex = Random.Range(0, availableObstacles.Count);
            ObstacleSO obstacleSO = availableObstacles[randomIndex];

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
