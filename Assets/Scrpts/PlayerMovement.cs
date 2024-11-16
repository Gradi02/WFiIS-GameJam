using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool canMove = true;
    public float moveSpeed;

    public bool isPlayerOne;

    public float buttonDelay = 3.0f; //the delay between button presses
    float lastButtonTime = 0; //cache the last pressed time

    [SerializeField] public GameLoopManager GLM;

    public bool isTable = false;

    public bool table = false;
    public bool isInvicible = false;
    


    private bool canDash;
    private bool isDashing = false;

    private float dashingPower = 2f;




    public Rigidbody2D rb;

    private Vector2 moveDirection;

    void Update()
    {
        if (!canMove) return;
        //if(isDashing)
        //{
        //    return;
        //}
        ProcessInputs();
    }

    void FixedUpdate()
    {
        if (!canMove) return;

        //if (isDashing)
        //{
        //    return;
        //}
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

            if (Input.GetKeyDown(KeyCode.LeftShift) && moveDirection != Vector2.zero && isTable==false)
            {
                Dash();
            }
        }
        else
        {
            moveX = Input.GetAxisRaw("HorizontalARROWS");
            moveY = Input.GetAxisRaw("VerticalARROWS");
            moveDirection = new Vector2(moveX, moveY).normalized; //This causes the diagonal speed to be normal

            if (Input.GetKeyDown(KeyCode.RightShift) && moveDirection != Vector2.zero && isTable == false)
            {
                Dash();
            }
        }

    }

    void Move()
    {
        if (isTable == false)
        {

            rb.linearVelocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);

        }
    }
    void Dash()
    {
        if (Time.time >= lastButtonTime)
        {
            //add the current time to the button delay
            lastButtonTime = Time.time + buttonDelay;
            
             transform.position += new Vector3(moveDirection.x, moveDirection.y, 0) * dashingPower;
        }
    }

    public void SetMovement(bool en)
    {
        canMove = en;
        rb.linearVelocity = Vector2.zero;
    }
}
