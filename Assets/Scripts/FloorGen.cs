using Unity.VisualScripting;
using UnityEngine;

public class FloorGen : MonoBehaviour
{
    [SerializeField] GameObject floorPrefab;
    [SerializeField] int floorAmount = 12;
    int floorLength ; //the length of the floor for placing

    [SerializeField] Transform floorParent; //parent to keep all instantiated floors in a single object parent
    [SerializeField] float moveSpeed = 10f;

    GameObject[] floorArray; //array to keep track of the floors created


    void Start()
    {
        floorLength = (int)floorPrefab.transform.GetChild(0).Find("Floor").localScale.z; //get the length of the floor from the prefab Object->model->floor
        floorArray = new GameObject[floorAmount];
        //floorLength = (int)floorPrefab.transform.Find("Model").Find("Floor").localScale.z; //redundant of the above but more readable
        //print("Floor Length: " + floorLength);
        GenerateFloor();
    }

    void Update()
    {
        MoveFloor();
    }

    //create floor with a block being created forward every 10 units (the size of the floor) and put in a parentGoup for organization
    private void GenerateFloor()
    {
        Vector3 newFloorPos; //new for the floor to be created at
        GameObject floor;
        for (int i = 0; i<floorAmount; i++)
        {
            newFloorPos = CalculateNewFloorPos(i); //calculate the new position based of previose

            floor = Instantiate(floorPrefab, transform.position + newFloorPos, Quaternion.identity, floorParent);
            floorArray[i] = floor;
 
        }
    }
    //Calculate new postion of Z based on length an num of floors already created
    private Vector3 CalculateNewFloorPos(int i)
    {
        return new Vector3(transform.position.x, transform.position.y, transform.position.z + floorLength * i);
    }
    //Move the floor backwards toward the player camera
    private void MoveFloor()
    {

        for (int i = 0; i < floorArray.Length; i++)
        {
            floorArray[i].transform.Translate(new Vector3(0,0,(-moveSpeed * Time.deltaTime)));
        }
    }
    
        
    
}
