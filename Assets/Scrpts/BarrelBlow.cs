using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    public GameObject[] explosionPrefabs;
    public float explosionForce = 5f;
    public float explosionDuration = 1f;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Explode();
    }

    private void Explode()
    {
        foreach (var prefab in explosionPrefabs)
        {
            GameObject fragment = Instantiate(prefab, transform.position, Quaternion.identity);

            Rigidbody2D rb = fragment.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 randomDirection = Random.insideUnitCircle.normalized;
                rb.AddForce(randomDirection * explosionForce, ForceMode2D.Impulse);
            }

            Destroy(fragment, explosionDuration);
        }

        Destroy(gameObject);
    }
}
