using System.Collections;
using UnityEngine;

public class BladeSwing : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Swing());
    }


    IEnumerator Swing()
    {
        while (true)
        {
            LeanTween.rotateZ(this.gameObject, -89, 1f).setEase(LeanTweenType.easeInOutSine);
            yield return new WaitForSeconds(1.1f);
            LeanTween.rotateZ(this.gameObject, 89, 1f).setEase(LeanTweenType.easeInOutSine);
            yield return new WaitForSeconds(1.1f);
        }
    }
}
