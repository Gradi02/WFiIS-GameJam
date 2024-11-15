using UnityEngine;

public class cameraManager : MonoBehaviour
{
    public Transform target;
    public bool isLeft;
    [SerializeField] private Camera cam;

    void Update()
    {
        if (target != null)
        {
            float dst = target.position.y;
            Vector3 camPos = new Vector3(isLeft == true ? -4.5f : 4.5f, Mathf.Clamp(dst, 0, 100), -10);
            transform.position = Vector3.Lerp(transform.position, camPos, 2f * Time.deltaTime);
        }
    }
}
