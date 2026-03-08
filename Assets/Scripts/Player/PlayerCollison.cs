using Unity.VisualScripting;
using UnityEngine;

public class PlayerCollison : MonoBehaviour
{
    float collisionCooldown = .75f;
    float coolDownTimer = 0f;
    [SerializeField] Animator animator;

    

    const string hitString = "Hit";//name of the animator trigger
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Update()
    {
        if ( coolDownTimer < collisionCooldown)
        {
       coolDownTimer += Time.deltaTime;
        }
    }
    void OnCollisionEnter(Collision collision)
    {

        if (collisionCooldown < coolDownTimer)
        {
        animator.SetTrigger(hitString); //Trigger the animator that is on player
        coolDownTimer = 0f;
        }
        return;
        
    }
}
