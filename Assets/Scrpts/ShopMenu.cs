using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class shopMenu : MonoBehaviour
{
    public int[,] shopItems = new int[8, 8];
    public float coins;
    public Text CoinsText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CoinsText.text = "Coins: " + coins.ToString();
        
        shopItems[1, 1] = 1;
        shopItems[1, 2] = 2;
        shopItems[1, 3] = 3;
        shopItems[1, 4] = 4;
        shopItems[1, 5] = 5;
        shopItems[1, 6] = 6;
        shopItems[1, 7] = 7;
        shopItems[1, 8] = 8;

        shopItems[2, 1] = 10;
        shopItems[2, 2] = 2;
        shopItems[2, 3] = 3;
        shopItems[2, 4] = 4;
        shopItems[2, 5] = 5;
        shopItems[2, 6] = 6;
        shopItems[2, 7] = 7;
        shopItems[2, 8] = 8;
    }

    public void Buy()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        if (coins >= shopItems[2,ButtonRef.GetComponent<ButtonInfo>().ItemID])
        {
            coins -= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID];
            CoinsText.text = "Coins: " + coins.ToString();
        }
    }
}
