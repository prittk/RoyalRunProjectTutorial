using UnityEngine;

public class CoinPickup : Pickup
{
    ScoreManager scoreManager;

    public void init(ScoreManager sg)
    {
        scoreManager = sg;
    }

    protected override void onPickUp()
    {
        Debug.Log("Player picked up a coin");
        scoreManager.SetScore(100);

    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created

}
