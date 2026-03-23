using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEditor.EditorTools;
using UnityEngine.PlayerLoop;
using Unity.VisualScripting.FullSerializer;
using Unity.VisualScripting;

public class FloorGen : MonoBehaviour
{
    [Header("Refences")]
    [SerializeField] CameraController cameraController;
    [SerializeField] GameObject[] floorPrefabs;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] int floorAmount = 10;
    [Tooltip("Do not change floorAmount unless it is the same as prefab tile size length")]

    int floorLength = 10 ; //the length of the floor for placing
    [SerializeField] Transform floorParent; //parent to keep all instantiated floors in a single object parent

    [Header("Min/Max Level Settings")]
    [SerializeField] float deafultMoveSpeed = 10f;
    [Tooltip("moveSpeed should be deault after boost ends")]
    [SerializeField] public float moveSpeed = 10f;
    [SerializeField] float minMoveSpeed = 5f;
    [SerializeField] float maxMoveSpeed = 15f;
    [SerializeField] float minGravityZ = -22f;
    [SerializeField] float maxGravityZ = -2f;

    int floorsSpawned = 0;
    [SerializeField] GameObject checkPointFloor;



    List<GameObject> floorList = new List<GameObject>();


    void Start()
    {
        //floorLength = (int)selectedChunk.transform.GetChild(0).Find("Floor").localScale.z; //get the length of the floor from the prefab Object->model->floor
        //floorLength = (int)floorPrefab.transform.Find("Model").Find("Floor").localScale.z; //redundant of the above but more readable
        //print("Floor Length: " + floorLength);
        GenerateFloor();
    }

    void Update()
    {
        MoveFloor();
    }

 


    public void changeMoveSpeed(float speedChange)
    {
       StartCoroutine(MoveSpeed(speedChange));
       

    }

    //Take new move speed and adjust settings based on move speed
    IEnumerator MoveSpeed(float speedChange)
    {
        float newMoveSpeed = moveSpeed + speedChange;
        newMoveSpeed = Mathf.Clamp(newMoveSpeed, minMoveSpeed, maxMoveSpeed);

        if (newMoveSpeed != moveSpeed)
        {
            cameraController.changeCameraFov(speedChange);

            moveSpeed = newMoveSpeed;

            adjustGravity(speedChange);
            yield return new WaitForSeconds(3f); //wait before reverting speed/grav/cameria
            
            moveSpeed = deafultMoveSpeed;
            adjustGravity(-speedChange);
            cameraController.changeCameraFov(-speedChange);
        }
        else
        {
            yield return null;
        }
       
        

        

        //  while(elapsedTime < duration)
        // {
        //     elapsedTime = Time.deltaTime;
        //     percentage = elapsedTime/duration;
        //     moveSpeed = Mathf.Lerp(startSpeed, newSpeed,percentage);
        //     yield return null;
        // }

        
    }

    public void adjustGravity(float adjustment)
    {
        float newGravity = Physics.gravity.z - adjustment;

        Mathf.Clamp(newGravity,minGravityZ,maxGravityZ);
        Physics.gravity = new Vector3(Physics.gravity.x, Physics.gravity.y, newGravity);

    }

    //create floor with a block being created forward by set amount (the size of the floor) and put in a parentGroup list for organization
    private void GenerateFloor()
    {
        Vector3 newFloorPos; //new for the floor to be created at

        for (int i = floorList.Count; i<floorAmount; i++)
        {
            GameObject floorGO;

            newFloorPos = CalculateNewFloorPos(); //calculate the new position based of previose
           

            floorGO= Instantiate(SelectedFloor(), transform.position + newFloorPos, Quaternion.identity,floorParent);
            floorList.Add(floorGO);

            FloorItems floorItems = floorGO.GetComponent<FloorItems>();//Script on floorPrefab that controls Item spawn
            floorItems.Init(this,scoreManager);//Intitialize scoreManager for score passing using only 1 manager thats created in this script

            floorsSpawned++;
            
 
        }
    }

    private GameObject SelectedFloor()
    {
        GameObject selectedFloor;
         if (floorsSpawned % floorAmount == 0 && floorsSpawned != 0)
            {
                selectedFloor = checkPointFloor;
            }
            else
            {
                selectedFloor = floorPrefabs[Random.Range(0,floorPrefabs.Length)];
            }
        return selectedFloor;
    }


    //Calculate new postion of Z based on length an num of floors already created
    private Vector3 CalculateNewFloorPos()
    {
        Vector3 spawnPos;
        if (floorList.Count == 0) //if there are no floors, create the first one at the spawner position
        {
        spawnPos = Vector3.zero;
        }
        else
        {
        spawnPos = new Vector3(transform.position.x, transform.position.y, floorList[floorList.Count - 1].transform.position.z + floorLength); //get last floor postion and add to end
        }
        return spawnPos;
    }
    //Move the floor backwards toward the player camera
    private void MoveFloor()
    {
        GameObject floor;
        for (int i = 0; i < floorList.Count; i++)
        {
            floor = floorList[i];
            floorList[i].transform.Translate(new Vector3(0, 0, -moveSpeed * Time.deltaTime));

            RemoveFloor(floor);
            GenerateFloor();

        }
    }
    //Remove a floor if it is outside the view of the camera
    private void RemoveFloor(GameObject floor)
    {
        if (floor.transform.position.z < Camera.main.transform.position.z - floorLength) //if the floor is behind the camera by more than its length, remove it
        {
            Destroy(floor);
            floorList.Remove(floor);
        }
    }

 

}
