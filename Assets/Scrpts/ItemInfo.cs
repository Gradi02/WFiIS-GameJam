using UnityEngine;
using TMPro;

public class ItemInfo : MonoBehaviour
{
    public int price;
    public GameObject obstaclePrefab;
    public GameObject selected;
    public TextMeshProUGUI priceText;

    private void Start()
    {
        priceText.text = price.ToString() + "$";
    }
}
