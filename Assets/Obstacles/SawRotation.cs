using UnityEngine;
using UnityEngine.Rendering;

public class SawRotation : MonoBehaviour
{
    public GameObject saw;
    private void Start()
    {
        LeanTween.rotateAround(this.gameObject, new Vector3(0,0,1), 360, 1f).setLoopCount(-1);
        LeanTween.rotateAround(saw, new Vector3(0, 0, 1), 360, 0.5f).setLoopCount(-1);
    }
}
