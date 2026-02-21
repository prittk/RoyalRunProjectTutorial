using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
public class ObstacleSpawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject[] obstacle;
    [SerializeField] Transform obstacleGroup;



    float spawnInterval = 2f;

    void Start()
    {
        StartCoroutine(SpawnObstacles());
    }

    private void Update() 
    {
        DeleteObstacle();
    }

    IEnumerator SpawnObstacles()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            Vector3 spawnPos = new Vector3(Random.Range(-4.25f,4.25f), transform.position.y, Random.Range(50f,90f));
            //spawn objects with a random x with the floor width, the y of the spawner, and the z of the lenght of the floorpath
            Instantiate(obstacle[Random.Range(0, obstacle.Length)], spawnPos, Random.rotation, obstacleGroup);
        }
    }

    private void DeleteObstacle()
    {
        foreach(Transform obstacle in obstacleGroup)
        {
            if (obstacle.position.z < Camera.main.transform.position.z - 10f || obstacle.position.y < -2f)
            {
                Destroy(obstacle.gameObject);
            }
        }
        
    }
}
