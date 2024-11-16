using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool canMove = false;
    public float moveSpeed;

    public bool isPlayerOne;

    [SerializeField] public GameLoopManager GLM;

    public bool isTable = false;

    public bool table = false;
    public bool isInvicible = false;
    private float endTable, endInv;
    public Sprite tableImg;
    public Color normal, alpha;

    private bool canDash;
    private bool isDashing = false;

    private float dashingPower = 2f;
    public Animator anim;


    public Rigidbody2D rb;

    private Vector2 moveDirection;

    void Update()
    {
        if(isTable)
        {
            endTable -= Time.deltaTime;
            if (endTable < 0)
            {
                isTable = false;
                GetComponent<Animator>().enabled = true;
            }
        }

        if (isInvicible)
        {
            endInv -= Time.deltaTime;
            if (endInv < 0)
            {
                isInvicible = false;
                GetComponent<BoxCollider2D>().enabled = true;
                GetComponent<SpriteRenderer>().color = normal;
            }
        }

        if (!canMove || isTable) return;
        //if(isDashing)
        //{
        //    return;
        //}
        ProcessInputs();
    }

    void FixedUpdate()
    {
        if (!canMove || isTable) return;

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
        rb.linearVelocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        anim.SetFloat("speed", rb.linearVelocity.magnitude);
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

    public void SetTable()
    {
        endTable = 3f;
        isTable = true;
        GetComponent<Animator>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = tableImg;
    }

    public void SetInv()
    {
        endInv = 3f;
        isInvicible = true;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().color = alpha;
    }
}
