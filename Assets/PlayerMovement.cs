using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool canMove = false;
    public float moveSpeed;

    public bool isPlayerOne;

    private bool canDash;
    private bool isDashing = false;

    private float dashingPower = 2f;



    public Rigidbody2D rb;

    private Vector2 moveDirection;


    /*TODO
    - Add Dashing upon pressing the Shit key
    */

    void Update()
    {
        if (!canMove) return;
        if(isDashing)
        {
            return;
        }
        ProcessInputs();
    }

    void FixedUpdate()
    {
        if (!canMove) return;

        if (isDashing)
        {
            return;
        }
        Move();
    }

    void ProcessInputs()
    {
        float moveX, moveY;
        if (isPlayerOne)
        {
            moveX = Input.GetAxisRaw("HorizontalWSAD");
            moveY = Input.GetAxisRaw("VerticalWSAD");
            moveDirection = new Vector2(moveX, moveY).normalized; //This causes the diagonal speed to be normal

            if (Input.GetKeyDown(KeyCode.LeftShift) && moveDirection != Vector2.zero)
            {
                Dash();
            }
        }
        else
        {
            moveX = Input.GetAxisRaw("HorizontalARROWS");
            moveY = Input.GetAxisRaw("VerticalARROWS");
            moveDirection = new Vector2(moveX, moveY).normalized; //This causes the diagonal speed to be normal

            if (Input.GetKeyDown(KeyCode.RightShift) && moveDirection != Vector2.zero)
            {
                Dash();
            }
        }

    }

    void Move()
    {
        rb.linearVelocity = new Vector2(moveDirection.x*moveSpeed, moveDirection.y*moveSpeed);
    }

    void Dash()
    {
        transform.position += new Vector3(moveDirection.x, moveDirection.y, 0) * dashingPower;
    }

    public void SetMovement(bool en)
    {
        canMove = en;
        rb.linearVelocity = Vector2.zero;
    }
}
