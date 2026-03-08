using System;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    const String playerString = "Player";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected abstract void onPickUp();
    void Update()
    {
        rotatePickup();
    }
    
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == playerString)
        {
            onPickUp();
            Destroy(gameObject);
        }
    }

    
   

    void rotatePickup()
    {
        transform.Rotate(new Vector3(0, 1 * Time.deltaTime, 0), 1);

    }
}
