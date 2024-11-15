using UnityEngine;

public class bulletBehaviour : MonoBehaviour
{
    GameObject target;
    public float speed;
    Rigidbody2D bulletRB;
    void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
        bulletRB.linearVelocity = new Vector2(moveDir.x, moveDir.y);
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        switch (hitInfo.gameObject.tag)
        {
            case "Player":
                Destroy(gameObject);
                break;
        }
    }
}
