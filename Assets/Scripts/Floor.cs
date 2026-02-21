using System.Collections.Generic;
using UnityEngine;


public class Floor : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] float[] lanes = {-4.5f, -1.25f, 2f}; //the x position of the lanes for spawning fences and obstacles

    [SerializeField] GameObject fencePrefab;

    void Start()
    {
        SpawnFence();
    }

    private void SpawnFence()
    {
        int numOfFences = Random.Range(0, lanes.Length); //random number of fences to spawn on the floor
        List<int> usableLanes = new List<int>{0,1,2};

        for (int i = 0; i< numOfFences; i++)
        {
        int randomLaneIndex = Random.Range(usableLanes[0], usableLanes.Count);
        Vector3 spawnPos = new Vector3(lanes[randomLaneIndex], transform.position.y, transform.position.z);
        Instantiate(fencePrefab, spawnPos, Quaternion.identity, this.transform);
        usableLanes.RemoveAt(randomLaneIndex);

        if (usableLanes.Count <= 0) break;
          
        }
        
        return;
        
    }
}
