using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float moveSpeed;

    public bool isPlayerOne;

    public Rigidbody2D rb;

    private Vector2 moveDirection;

    void Update()
    {
        ProcessInputs();
    }

    void FixedUpdate()
    {
        Move();
    }

    void ProcessInputs()
    {
        float moveX, moveY;
        if (isPlayerOne)
        {
            moveX = Input.GetAxisRaw("HorizontalWSAD");
            moveY = Input.GetAxisRaw("VerticalWSAD");
        }
        else
        {
            moveX = Input.GetAxisRaw("HorizontalARROWS");
            moveY = Input.GetAxisRaw("VerticalARROWS");
        }

        moveDirection = new Vector2(moveX, moveY).normalized; //This causes the diagonal speed to be normal
    }

    void Move()
    {
        rb.linearVelocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
}
