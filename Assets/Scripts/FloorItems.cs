using System.Collections.Generic;
using UnityEngine;


public class FloorItems : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] float[] lanes = {-4.5f, -1.25f, 2f}; //the x position of the lanes for spawning fences and obstacles
    List<int> usableLanes = new List<int>{0,1,2};
    [SerializeField] GameObject fencePrefab;

    [SerializeField] GameObject applePrefab;
    [SerializeField] GameObject coinPrefab;

    


    void Start()
    {
        SpawnFences();
        SpawnApple();
        SpawnCoin();
        
      

    }

    private void SpawnFences()
    {
        int numOfFences = Random.Range(0, lanes.Length); //random number of fences to spawn on the floor
        

        for (int i = 0; i< numOfFences; i++)
        {
            if (usableLanes.Count <= 0) break;
            
            int laneAvialable = Selectlane();
            Vector3 spawnPos = new Vector3(lanes[laneAvialable], transform.position.y, transform.position.z);

            Instantiate(fencePrefab, spawnPos, Quaternion.identity, this.transform);

     
        }
        return;

    }
    //Spawn a random amount of numbers to pickup off the floor
    private void SpawnApple()
    {
        float spawnChance = .3f;
        SpawnObject(applePrefab, spawnChance);
    }
    
    private void SpawnCoin()
    {
       float spawnChance = .5f;
       SpawnObject(coinPrefab, spawnChance);
    }

    private void SpawnObject(GameObject prefab, float spawnChance)
    {
        if (usableLanes.Count <= 0 || Random.value > spawnChance) return;

        Vector3 spawnPos = CalculatePickupSpawn();
        int amountOfPickups = Random.Range(1,5);
        for (int i = 0; i < amountOfPickups; i++)
        {
            Instantiate(prefab, spawnPos + new Vector3(0, 0, i), Quaternion.identity, this.transform); //spawn amount of pickups going forward
        }

        return ;
    }


    private Vector3 CalculatePickupSpawn()
    {
        
        int laneAvialable = Selectlane();
        float xoffset = lanes[laneAvialable] + 1;
        Vector3 spawnPos = new Vector3(xoffset, transform.position.y + 1, transform.position.z -1f); //spawn at 1 open lane with a fixed position higher than the floor
        return spawnPos;
        
    }
    private int Selectlane()
    {
        int Lane;
        
        int randomLaneIndex = Random.Range(0, usableLanes.Count);
        Lane = usableLanes[randomLaneIndex];
        usableLanes.RemoveAt(randomLaneIndex);

        return Lane;


        
    }


        
    
        
    
}
