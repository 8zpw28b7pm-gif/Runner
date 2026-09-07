using System.Collections.Generic;
using RF.Core;
using RF.Obstacles;
using UnityEngine;

public class ObstaclePooler : MonoBehaviour
{
    [SerializeField] private ObstacleListSO obstacleListSO;
    [SerializeField] private int amountToSpawn = 5;
    [SerializeField] private Transform activeObstaclesContainer;

    [SerializeField] private List<Obstacle> obstacles;

    private int maxPoolSize = 0;

    private void Awake()
    {
        GameManager.Instance.ObstaclePooler = this;
    }
    private void Start()
    {
        InitialisePool();
    }

    private void InitialisePool()
    {
        maxPoolSize = obstacleListSO.GetObstacleList().Length * amountToSpawn;

        foreach (var obstacle in obstacleListSO.GetObstacleList())
        {
            for (int i = 0; i < amountToSpawn; i++)
            {
                Obstacle spawnedObstacle = obstacle.Spawn();
                spawnedObstacle.transform.SetParent(transform);
                obstacles.Add(spawnedObstacle);
                if (spawnedObstacle.gameObject.activeSelf)
                {
                    spawnedObstacle.gameObject.SetActive(false);
                }
            }
        }
    }

    public Obstacle GrabFromPool(ObstacleSO obstacleSO)
    {
        foreach (Transform child in transform)
        {
            Obstacle obstacle = child.GetComponent<Obstacle>();

            if (obstacle.GetObstacleSO() == obstacleSO)
            {
                obstacle.transform.SetParent(activeObstaclesContainer);
                obstacle.gameObject.SetActive(true);
                obstacles.Remove(obstacle);
                return obstacle;
            }
        }

        return obstacleSO.Spawn();
    }

    public void ReturnToPool(Obstacle obstacle)
    {
        obstacle.transform.SetParent(transform);
        obstacle.gameObject.SetActive(false);
    }
}
