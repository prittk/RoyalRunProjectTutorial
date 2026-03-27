using System;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    const String player = "Player";
    TimeManager timeManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeManager = FindFirstObjectByType<TimeManager>();
    }
    
    void OnTriggerEnter(Collider other)
    {
      if(other.gameObject.tag == player)
        {
            timeManager.increaseTime(10f);
        }
    }

}
