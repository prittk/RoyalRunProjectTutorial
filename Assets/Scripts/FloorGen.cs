using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FloorGen : MonoBehaviour
{
    [SerializeField] GameObject floorPrefab;
    [SerializeField] int floorAmount = 12;
    int floorLength ; //the length of the floor for placing

    [SerializeField] Transform floorParent; //parent to keep all instantiated floors in a single object parent
    [SerializeField] float moveSpeed = 10f;

    List<GameObject> floorList = new List<GameObject>();


    void Start()
    {
        floorLength = (int)floorPrefab.transform.GetChild(0).Find("Floor").localScale.z; //get the length of the floor from the prefab Object->model->floor
        //floorLength = (int)floorPrefab.transform.Find("Model").Find("Floor").localScale.z; //redundant of the above but more readable
        //print("Floor Length: " + floorLength);
        GenerateFloor();
    }

    void Update()
    {
        MoveFloor();
    }

    //create floor with a block being created forward by set amount (the size of the floor) and put in a parentGroup list for organization
    private void GenerateFloor()
    {
        Vector3 newFloorPos; //new for the floor to be created at
        GameObject floor;
        for (int i = floorList.Count; i<floorAmount; i++)
        {
            newFloorPos = CalculateNewFloorPos(); //calculate the new position based of previose

            floor = Instantiate(floorPrefab, transform.position + newFloorPos, Quaternion.identity, floorParent);
            floorList.Add(floor);
 
        }
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
