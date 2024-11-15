using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInfo : MonoBehaviour
{
    public int ItemID;
    public Text PriceText;
    public GameObject ShopManager;
   void Start()
    {
        PriceText.text = "Price: " + ShopManager.GetComponent<shopMenu>().shopItems[2, ItemID].ToString();
    }
}
