using UnityEngine;
using TMPro;

[System.Serializable]
public class ItemData
{
    public string itemName; // Nama GameObject item (A, B, C, D, E)
    public int sellValue;   // Nilai jual item
}

public class EconomyManager : MonoBehaviour
{
    public static EconomyManager Instance;

    [Header("Item Database")]
    [SerializeField] private ItemData[] itemDatabase; // Isi data item di Inspector

    [Header("Money Settings")]
    [SerializeField] private int currentMoney;
    [SerializeField] private TextMeshProUGUI moneyText;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateMoneyUI();
    }

    // Cari nilai jual item berdasarkan nama GameObject
    public int GetSellValue(string itemName)
    {
        foreach (ItemData item in itemDatabase)
        {
            if (item.itemName == itemName)
            {
                return item.sellValue;
            }
        }
        Debug.LogError("Item tidak ditemukan di database: " + itemName);
        return 0;
    }

    public void AddMoney(int amount)
    {
        currentMoney += amount;
        UpdateMoneyUI();
    }

    private void UpdateMoneyUI()
    {
        moneyText.text = currentMoney.ToString();
    }
}