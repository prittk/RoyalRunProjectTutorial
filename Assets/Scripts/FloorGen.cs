using Unity.VisualScripting;
using UnityEngine;

public class FloorGen : MonoBehaviour
{
    [SerializeField] GameObject floorPrefab;
    [SerializeField] int floorAmount = 10;
    int floorLength ; //the length of the floor for placing

    [SerializeField] Transform floorParent; //parent to keep all instantiated floors in a single object parent

    void Start()
    {
        floorLength = (int)floorPrefab.transform.GetChild(0).GetChild(0).localScale.z; //get the length of the floor from the prefab Object->model->floor
        //floorLength = (int)floorPrefab.transform.Find("Model").Find("Floor").localScale.z; //redundant of the above but more readable
        //print("Floor Length: " + floorLength);
        generateFloor();
    }

    //create floor with a block being created forward every 10 units (the size of the floor) and put in a parentGoup for organization
    private void generateFloor()
    {
        for (int i = 0; i<floorAmount; i++)
        {
            Instantiate(floorPrefab, transform.position + new Vector3(0, 0, i * floorLength), Quaternion.identity,floorParent);

        }


    }
        
    
}
