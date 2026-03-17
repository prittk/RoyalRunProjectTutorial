using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Rendering.Universal;
using UnityEngine;


public class FloorItems : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] float[] lanes = {-4.5f, -1.25f, 2f}; //the x position of the lanes for spawning fences and obstacles
    List<int> usableLanes = new List<int>{0,1,2};
    [Header("items to run into prefabs")]
    [SerializeField] GameObject fencePrefab;

    [SerializeField] GameObject applePrefab;
    [SerializeField] GameObject coinPrefab;

    FloorGen floorGen;
    ScoreManager scoreManager;

    


    void Start()
    {
        SpawnFences();
        SpawnApple();
        SpawnCoin();

    }

    public void Init(FloorGen fg, ScoreManager sm)
    {
       this.floorGen = fg;
       this.scoreManager = sm;
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
        if (usableLanes.Count <= 0 || Random.value > spawnChance) return;

        Vector3 spawnPos = CalculatePickupSpawn();
        int amountOfPickups = Random.Range(1,5);
        for (int i = 0; i < amountOfPickups; i++)
        {
            ApplePickup newApple = Instantiate(applePrefab, spawnPos + new Vector3(0, 0, i), Quaternion.identity, this.transform).GetComponent<ApplePickup>(); //spawn amount of pickups going forward
            newApple.init(floorGen);
        }
    }
    
    private void SpawnCoin()
    {
        float spawnChance = .5f;
        if (usableLanes.Count <= 0 || Random.value > spawnChance) return;

        Vector3 spawnPos = CalculatePickupSpawn();
        int amountOfPickups = Random.Range(1,5);
        for (int i = 0; i < amountOfPickups; i++)
        {
            CoinPickup newCoin = Instantiate(coinPrefab, spawnPos + new Vector3(0, 0, i), Quaternion.identity, this.transform).GetComponent<CoinPickup>(); //spawn amount of pickups going forward
            newCoin.init(scoreManager);
        }

    }

    // private void SpawnObject(GameObject prefab, float spawnChance, GameObject gameObject)
    // {
    //     if (usableLanes.Count <= 0 || Random.value > spawnChance) return;

    //     Vector3 spawnPos = CalculatePickupSpawn();
    //     int amountOfPickups = Random.Range(1,5);
    //     for (int i = 0; i < amountOfPickups; i++)
    //     {
    //         GameObject newObject = Instantiate(prefab, spawnPos + new Vector3(0, 0, i), Quaternion.identity, this.transform).GetComponent<CoinPickup>();; //spawn amount of pickups going forward
    //         newObject.GetComponent<GameObject>.init(FloorGen);
    //     }

    //     return ;
    // }


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
