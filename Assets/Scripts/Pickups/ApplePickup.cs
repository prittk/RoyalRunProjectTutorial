using UnityEngine;


    
public class ApplePickup : Pickup 
{
    FloorGen FloorGen;
    [SerializeField] float adjustMoveSpeed = 1f;
    void Start()
    {
        FloorGen = FindFirstObjectByType<FloorGen>();
    }
    
    protected override void onPickUp()
    {
        Debug.Log("Player picked up an apple");
        FloorGen.changeMoveSpeed(adjustMoveSpeed);
    }
}