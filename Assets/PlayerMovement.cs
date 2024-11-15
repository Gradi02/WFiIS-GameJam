using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool canMove = true;
    public float moveSpeed;

    public bool isPlayerOne;

    private bool canDash;
    private bool isDashing = false;

    private float dashingPower = 20f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;



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
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Dash();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
               Dash();
            }
            moveX = Input.GetAxisRaw("HorizontalARROWS");
            moveY = Input.GetAxisRaw("VerticalARROWS");
        }

        moveDirection = new Vector2(moveX, moveY).normalized; //This causes the diagonal speed to be normal
    }

    void Move()
    {
        rb.linearVelocity = new Vector2(moveDirection.x*moveSpeed, moveDirection.y*moveSpeed);
    }

    void Dash()
    {
        //moveDirection += (new Vector2(5, 5));
    }

    public void SetMovement(bool en)
    {
        canMove = en;
        rb.linearVelocity = Vector2.zero;
    }
}
