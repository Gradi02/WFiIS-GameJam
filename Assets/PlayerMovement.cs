using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool canMove = false;
    public float moveSpeed;

    public bool isPlayerOne;

    private float spd;

    private bool canDash;
    private bool isDashing = false;

    private float dashingPower = 20f;
    private float dashingTime = 2f;
    private float dashingCooldown = 1f;



    public Rigidbody2D rb;

    private Vector2 moveDirection;


    /*TODO
    - Add Dashing upon pressing the Shit key
    - Add a bool, if the building ability is active, it will switch off any movement
    - 
    */

    void Update()
    {
        if (!canMove) return;
/*        if(isDashing)
        {
            return;
        }*/
        ProcessInputs();
        spd = Mathf.Abs(moveDirection.magnitude * moveSpeed);
/*        if(Input.GetKeyDown(KeyCode.LeftShift) && canDash==true)
        {
            StartCoroutine(Dash());
        }*/
    }

    void FixedUpdate()
    {
        if (!canMove) return;

        /*        if (isDashing)
                {
                    return;
                }*/
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
        rb.linearVelocity = new Vector2(moveDirection.x*moveSpeed, moveDirection.y*moveSpeed);
    }

/*    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        rb.linearVelocity = new Vector2(transform.localScale.x*dashingPower,0f);
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }*/

    public void SetMovement(bool en)
    {
        canMove = en;
        rb.linearVelocity = Vector2.zero;
    }
}
