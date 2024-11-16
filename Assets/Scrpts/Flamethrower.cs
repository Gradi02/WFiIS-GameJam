using UnityEngine;

public class Flamethrower : MonoBehaviour
{
    public GameObject[] flamePrefab;
    public Transform flameSpawnPoint;
    public Transform player;
    public float flameSpeed = 10f;
    public float shootInterval = 0.5f;
    public float flameLifetime = 5f;
    public float flameScale = 2f;

    private float shootTimer;

    void Start()
    {
        shootTimer = shootInterval;
    }

    void Update()
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0f)
        {
            ShootFlame();
            shootTimer = shootInterval;
        }
    }

    void ShootFlame()
    {
        if (player == null) return;

        Vector2 directionToPlayer = (player.position - flameSpawnPoint.position).normalized * Random.Range(0.8f, 1.2f);

        GameObject flame = Instantiate(flamePrefab[Random.Range(0, flamePrefab.Length)], flameSpawnPoint.position, Quaternion.identity);
        flame.transform.localScale *= flameScale;
        Rigidbody2D rb = flame.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = flame.AddComponent<Rigidbody2D>();
        }
        rb.gravityScale = 0;
        rb.linearVelocity = directionToPlayer * flameSpeed;

        flame.AddComponent<FlameCollisionHandler>();

        Destroy(flame, flameLifetime);
    }
}

public class FlameCollisionHandler : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(GameLoopManager.instance.ResetPlr(collision.gameObject));
        }

        Destroy(gameObject);
    }
}
