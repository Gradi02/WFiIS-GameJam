using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed;

    public bool isPlayerOne;

    private float spd;

    private bool canDash;
    private bool isDashing;

    private float dashingPower = 20f;
    private float dashingTime = 0.2f;
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
        if (isDashing)
        {
            return;
        }
        ProcessInputs();
        spd = Mathf.Abs(moveDirection.magnitude * moveSpeed);
        if(Input.GetKeyDown(KeyCode.LeftShift) && canDash==true)
        {
            StartCoroutine(Dash());
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
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
        rb.linearVelocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        rb.linearVelocity = new Vector2(0f, transform.localScale.y * dashingPower);
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
