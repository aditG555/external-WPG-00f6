using UnityEngine;
using TMPro;
using System;

[System.Serializable]
public class ItemData
{
    public Jamu.jamuType itemName; // Ganti dari Jamu[] ke enum
    public int sellValue;
}

public class EconomyManager : MonoBehaviour
{
    public static EconomyManager Instance;

    [Header("Item Database")]
    [SerializeField] private ItemData[] itemDatabase;

    [Header("Money Settings")]
<<<<<<< Updated upstream
    [SerializeField] private int currentMoney;
    [SerializeField] private TextMeshProUGUI moneyText;
=======
    [SerializeField] private TextMeshProUGUI moneyText;
    public static int currentMoney;

    public int Popularity = 1;
>>>>>>> Stashed changes

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
        //DontDestroyOnLoad(this);
    }
    void Start()
    {
        UpdateMoneyUI();
        Debug.Log("Economy Start() is Triggered");
    }

    public int GetSellValue(Jamu.jamuType itemName)
    {
        foreach (ItemData item in itemDatabase)
        {
            if (item.itemName == itemName)
            {
                return item.sellValue;
            }
        }
        Debug.LogError("Item not found: " + itemName);
        return 0;
    }

    public void AddMoney(int amount)
    {
        currentMoney += amount;
        UpdateMoneyUI();
    }

    public void RemoveMoney(int amount)
    {
        currentMoney = Mathf.Max(currentMoney - amount, 0);
        UpdateMoneyUI();
    }

    public int GetJamuBasePrice(GameObject jamuItem)
    {
        Jamu jamu = jamuItem.GetComponent<Jamu>();
        if(jamu == null) { return 0; }
        foreach (ItemData item in itemDatabase)
        {
            if (item.itemName == jamu.type)
            {
                return item.sellValue;
            }
            {
                return item.sellValue;
            }
        }
        return 0;
    }

    public void ProcessJamuTransaction(NPCTrait[] traits, bool isCorrect, int baseValue)
    {
        int finalAmount = baseValue;
        
        foreach(NPCTrait trait in traits)
        {
            if(isCorrect)
            {
                if(trait == NPCTrait.Generous) finalAmount += 5;
                if(trait == NPCTrait.Forgetful) finalAmount += 2;
                Popularity++;
            }
            else
            {
                if(trait == NPCTrait.Perfectionist) finalAmount *= 2;
                if(trait == NPCTrait.Grumpy) finalAmount += 10;
                Popularity--;
            }
        }

        Debug.Log($"Transaksi: {(isCorrect ? "+" : "-")}{finalAmount}");

        if (isCorrect)
        {
            AddMoney(finalAmount);
            Popularity++;
        }
        else RemoveMoney(finalAmount);
    }

    public void UpdateMoneyUI()
    {
        moneyText.text = currentMoney.ToString();
    }

    public void ProcessRefund(int amount)
{
    currentMoney = Mathf.Max(currentMoney - amount, 0);
    UpdateMoneyUI();
}
}