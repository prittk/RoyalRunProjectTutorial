using UnityEditor.Callbacks;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;

    

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Rigidbody rb;
    Vector2 movement;
    [SerializeField] float moveSpeed = 10f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        MovePlayer(new Vector3(movement.x, 0 , movement.y));
    }

    public void Move(InputAction.CallbackContext context)
    {
        while (context.performed)
        {
        movement = context.ReadValue<Vector2>();
        }
        
        movement = Vector2.zero;
        Debug.Log(movement);
    }

    public void MovePlayer(Vector3 direction)
    {
        rb.AddForce(direction * moveSpeed *Time.deltaTime);
    }

    


}
