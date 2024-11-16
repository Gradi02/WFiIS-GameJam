using UnityEngine;

public class Flamethrower : MonoBehaviour
{
    public GameObject[] flamePrefab; // Prefab płomienia
    public Transform flameSpawnPoint; // Punkt, z którego płomień będzie wystrzeliwany
    public Transform player; // Referencja do obiektu gracza
    public float flameSpeed = 10f; // Prędkość płomienia
    public float shootInterval = 0.5f; // Interwał czasowy między strzałami
    public float flameLifetime = 5f; // Czas życia płomienia
    public float flameScale = 2f; // Skala płomienia

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

        // Oblicz kierunek w stronę gracza
        Vector3 directionToPlayer = (player.position - flameSpawnPoint.position).normalized * Random.Range(0.8f, 1.2f);

        // Tworzenie instancji płomienia
        GameObject flame = Instantiate(flamePrefab[Random.Range(0, flamePrefab.Length)], flameSpawnPoint.position, Quaternion.LookRotation(directionToPlayer));
        flame.transform.localScale *= flameScale; // Zwiększ skalę płomienia
        flame.transform.rotation = Quaternion.Euler(0, 0, 0);
        Rigidbody rb = flame.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = flame.AddComponent<Rigidbody>();
        }
        // Wyłączenie wpływu grawitacji na płomień
        rb.useGravity = false;
        // Nadanie prędkości płomieniowi w kierunku gracza
        rb.linearVelocity = directionToPlayer * flameSpeed;

        // Zniszczenie płomienia po określonym czasie
        Destroy(flame, flameLifetime);
    }
}
