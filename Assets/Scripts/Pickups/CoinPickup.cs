using UnityEngine;

public class CoinPickup : Pickup
{

    protected override void onPickUp()
    {
        Debug.Log("Player picked up a coin");    
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created

}
