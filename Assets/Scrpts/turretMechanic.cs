using UnityEngine;

public class turretMechanic : MonoBehaviour
{
    public float speed;
    public float lineOfSight;
    public float shootingRange;
    public float fireRate = 2f;
    public float nextFireTime;
    public GameObject Bullet;
    public GameObject FirePoint;

    [SerializeField] float maxDistance;

    Vector2 wayPoint;

    private Transform player; 
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer <= shootingRange && nextFireTime < Time.time)
        {
            Instantiate(Bullet, FirePoint.transform.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate;
        }
    }
}
