using UnityEngine;

public class Budowanie : MonoBehaviour
{
    public GameObject[] obstacles;

    public void Buduj()
    {
        Instantiate(obstacles[0], new Vector3(0,0,0) , Quaternion.identity);
    }
}
