using UnityEngine;



    
public class ApplePickup : Pickup 
{
    FloorGen FloorGen;
    [SerializeField] float adjustMoveSpeed = 1f;

    public void init(FloorGen fg)
    {
        FloorGen = fg;
    }
    
    protected override void onPickUp()
    {
        Debug.Log("Player picked up an apple");
        FloorGen.changeMoveSpeed(adjustMoveSpeed);
    }
}