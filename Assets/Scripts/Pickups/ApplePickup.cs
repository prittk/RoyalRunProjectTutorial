using UnityEngine;


    
public class ApplePickup : Pickup 
{
    
    protected override void onPickUp()
    {
        Debug.Log("Player picked up an apple");
    }
}