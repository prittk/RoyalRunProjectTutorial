
using UnityEngine;
using UnityEngine.InputSystem;

    

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Rigidbody rb;
    Vector2 movement;
    [SerializeField] float moveSpeed = 5f;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        MovePlayer(new Vector3(movement.x, 0f , movement.y));
    }

    public void Move(InputAction.CallbackContext context)
    {
        
        movement = context.ReadValue<Vector2>();
        Debug.Log(movement);
    }

    public void MovePlayer(Vector3 direction)
    {
        Vector3 currentPos = rb.position;
        Vector3 moveDir = currentPos + direction * (moveSpeed *Time.fixedDeltaTime);
        rb.MovePosition(moveDir);
    }

    


}
