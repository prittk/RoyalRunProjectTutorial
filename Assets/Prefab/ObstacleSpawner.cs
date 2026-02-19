using UnityEngine;
using System.Collections;
public class ObstacleSpawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject obstacle;
    [SerializeField] Transform obstacleGroup;

    int maxSpawnedObstacles = 10;

    float spawnInterval = 1f;

    void Start()
    {
        StartCoroutine(SpawnObstacles());
    }

    IEnumerator SpawnObstacles()
    {
        while (obstacleGroup.childCount < maxSpawnedObstacles)
        {
            yield return new WaitForSeconds(spawnInterval);
            Instantiate(obstacle, new Vector3(Random.Range(-4.25f, 4.25f), 1f, Random.Range(10f,20f)), Quaternion.identity,obstacleGroup);
        }
    }
}
