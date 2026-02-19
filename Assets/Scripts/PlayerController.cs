
using UnityEngine;
using UnityEngine.InputSystem;

    

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Rigidbody rb;
    Vector2 movement;

    [SerializeField]float clampedX = 4.25f;
    [SerializeField] float clampedZ = 3f;

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
        moveDir = new Vector3(Mathf.Clamp(moveDir.x, -clampedX, clampedX), moveDir.y, Mathf.Clamp(moveDir.z, -clampedZ, clampedZ));

        rb.MovePosition(moveDir);
    }

   

    


}
