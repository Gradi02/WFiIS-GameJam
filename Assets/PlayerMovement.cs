using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed;

    private float spd;

    public Rigidbody2D rb;

    private Vector2 moveDirection;


    void Start()
    {
        
    }

    void Update()
    {
        ProcessInputs();
        spd = Mathf.Abs(moveDirection.magnitude * moveSpeed);

    }

    void FixedUpdate()
    {
        Move();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float MoveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, MoveY).normalized; //This causes the diagonal speed to be normal
    }

    void Move()
    {
        rb.linearVelocity = new Vector2(moveDirection.x*moveSpeed, moveDirection.y*moveSpeed);
    }
}
