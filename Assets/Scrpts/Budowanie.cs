using UnityEngine;

public class Budowanie : MonoBehaviour
{
    public GameObject[] obstacles;

    public GameObject Buduj()
    {
        return Instantiate(obstacles[Random.Range(0, obstacles.Length)], new Vector3(0,0,0) , Quaternion.identity);
    }
}
