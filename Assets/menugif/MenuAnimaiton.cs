using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MenuAnimaiton : MonoBehaviour
{
    public Sprite[] menu;
    void Start()
    {
        StartCoroutine(Anime());
    }

    IEnumerator Anime()
    {
        while (true)
        {
            for(int i = 0; i < menu.Length; i++)
            {
                GetComponent<Image>().sprite = menu[i];
                yield return new WaitForSeconds(0.15f);
            }
        }
    }
}
