using System.Collections;
using UnityEngine;

public class Kolce : MonoBehaviour
{
    public GameObject[] kolce;
    private float[] initialYPositions;
    void Start()
    {
        initialYPositions = new float[kolce.Length];
        for (int i = 0; i < kolce.Length; i++)
        {
            initialYPositions[i] = kolce[i].transform.localPosition.y;
        }

        StartCoroutine(Kolcowanie());
    }

    IEnumerator Kolcowanie()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            for (int i = 0; i < kolce.Length; i++)
            {
                LeanTween.scaleY(kolce[i], 5, 0.2f);
                LeanTween.moveLocalY(kolce[i], initialYPositions[i] + 0.58f, 0.2f);
            }

            yield return new WaitForSeconds(1f);
            for (int i = 0; i < kolce.Length; i++)
            {
                LeanTween.scaleY(kolce[i], 1, 0.2f);
                LeanTween.moveLocalY(kolce[i], initialYPositions[i], 0.2f);
            }
        }
    }
}
