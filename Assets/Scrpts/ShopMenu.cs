using UnityEngine;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    public int coins = 100;
    public Text coinsText;
    public Button[] itemButtons;
    public int[] itemPrices;

    void Start()
    {
        UpdateCoinsText();
        SetupShop();
    }

    void UpdateCoinsText()
    {
        coinsText.text = "Coins: " + coins;
    }

    void SetupShop()
    {
        for (int i = 0; i < itemButtons.Length; i++)
        {
            int index = i;
            itemButtons[i].GetComponentInChildren<Text>().text = "PRICE: " + itemPrices[i];
            itemButtons[i].onClick.AddListener(() => BuyItem(index));
        }
    }

    void BuyItem(int index)
    {
        if (coins >= itemPrices[index])
        {
            coins -= itemPrices[index];
            UpdateCoinsText();
        }
    }
}
