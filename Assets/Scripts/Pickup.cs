using System;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    const String playerString = "Player";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == playerString)
        {
            Debug.Log("Player picked up " + gameObject.name);
            Destroy(gameObject);
        }
    }
}
