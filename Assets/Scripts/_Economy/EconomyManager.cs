using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;

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
    public int currentMoney;
    [SerializeField] private TextMeshProUGUI moneyText;
    private int dailyMoneyDelta = 0;
    public int Popularity = 1;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateMoneyUI();
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

    public int GetDailyMoney()
    {
        return dailyMoneyDelta;
    }

    public void ProcessJamuTransaction(NPCTrait[] traits, bool isCorrect, int baseValue)
    {
        int finalAmount = baseValue;

        if (!isCorrect)
        {
            Popularity--;
        }
        else
        {
            foreach (NPCTrait trait in traits)
            {
                switch (trait)
                {
                    case NPCTrait.Perfectionist:
                        finalAmount *= 2;
                        break;
                    case NPCTrait.Generous:
                        finalAmount += 5;
                        break;
                    case NPCTrait.Forgetful:
                        finalAmount += 2;
                        break;
                }
            }
            Popularity++;
        }

        AddMoney(finalAmount); 

        Debug.Log($"Transaksi: {finalAmount}");

        // else AddMoney(baseValue);

        dailyMoneyDelta += finalAmount;
        
        
        UpdateMoneyUI(); 
    }

    private void UpdateMoneyUI()
    {
        moneyText.text = currentMoney.ToString();
    }

    public void ProcessRefund(int amount)
    {
        currentMoney = Mathf.Max(currentMoney - amount, 0);
        UpdateMoneyUI();
    }
    public void ResetDailyMoney()
    {
        dailyMoneyDelta = 0;
    }
}