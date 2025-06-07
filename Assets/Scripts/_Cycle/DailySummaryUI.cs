using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEditor;

public class DailySummaryUI : MonoBehaviour
{
    [Header("UI Summary Elements")]
    [SerializeField] private GameObject summaryPanel;
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private TextMeshProUGUI totalServedText;
    [SerializeField] private TextMeshProUGUI correctServedText;
    [SerializeField] private TextMeshProUGUI moneyEarnedText;
    [SerializeField] private TextMeshProUGUI ratingText;
    [SerializeField] private Button nextDayButton;

    [Header("Rating Elements")]
    [SerializeField] private GameObject[] starIcons;

    [Header("UI Shop Elements")]
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private Button shopButton;
    [SerializeField] private TextMeshProUGUI moneyTotal;
    [SerializeField] private TextMeshProUGUI moneyBuy;
    [SerializeField] private TextMeshProUGUI moneyRest;
    [SerializeField] private Button nextDayButtonShop;
    
    
    [Header("Item Database")]
    [SerializeField] private ShoppingList[] shoppingList;
    [System.Serializable]
    public struct ShoppingList
    {
        public string itemName;
        public TextMeshProUGUI itemNameViewport;
        public TextMeshProUGUI counter;
        public Button decrement;
        public Button increment;
        public int price;
        public ShelveItme shelveItme;

    }
    private int initialMoney;
    private int totalBelanja;

    void Start()
    {
        nextDayButton.onClick.AddListener(StartNewDay);
        nextDayButtonShop.onClick.AddListener(StartNewDay);
        summaryPanel.SetActive(false);
        shopPanel.SetActive(false);
        shopButton.onClick.AddListener(ShowShop);
    }

    public void ShowSummary(int day, int total, int correct, int money, float rating)
    {
        summaryPanel.SetActive(true);
        dayText.text = $"Day {day} Summary";
        totalServedText.text = $"{total}";
        correctServedText.text = $"{correct}";
        moneyEarnedText.text = $"{money}";
        ratingText.text = $"{rating:F1}";

        // UpdateStarRating(rating);
    }

    void StartNewDay()
    {
        summaryPanel.SetActive(false);
        shopPanel.SetActive(false);
        DayCycleManager.Instance.StartNewDay();
        
    }
    public void ShowShop()
    {
        shopPanel.SetActive(true);
        summaryPanel.SetActive(false);

        initialMoney = EconomyManager.Instance.currentMoney;
        totalBelanja = 0;

        moneyTotal.text = $"{EconomyManager.Instance.currentMoney}";
        foreach (var item in shoppingList)
        {
            item.itemNameViewport.text = item.itemName;
            item.counter.text = "0";
            item.decrement.onClick.AddListener(() => DecrementItem(item));
            item.increment.onClick.AddListener(() => IncrementItem(item));
        }
    }
    public void IncrementItem(ShoppingList item)
    {
        int sisaUang = initialMoney - totalBelanja;
        
        if (sisaUang >= item.price)
        {
            // Update counter item
            int counter = int.Parse(item.counter.text);
            counter++;
            item.counter.text = counter.ToString();

            //Update Item Count di ShelveItme
            if (item.shelveItme != null)
            {
                item.shelveItme.AddCount(1);
            }
            
            // Update total belanja
            totalBelanja += item.price;
            
            // Update UI
            moneyBuy.text = totalBelanja.ToString();
            moneyRest.text = (initialMoney - totalBelanja).ToString();
            
            // Kurangi uang di sistem ekonomi
            EconomyManager.Instance.RemoveMoney(item.price);
        }
        else
        {
            Debug.Log("Not enough money to buy this item.");
        }
    }
    public void DecrementItem(ShoppingList item)
    {
        int counter = int.Parse(item.counter.text);
        if (counter > 0)
        {
            // Update counter item
            counter--;
            item.counter.text = counter.ToString();

            // Update Item Count di ShelveItme
            if (item.shelveItme != null)
            {
                item.shelveItme.MinCount(1);
            }
            
            // Update total belanja
            totalBelanja -= item.price;
            
            // Update UI
            moneyBuy.text = totalBelanja.ToString();
            moneyRest.text = (initialMoney - totalBelanja).ToString();
            
            // Kembalikan uang di sistem ekonomi
            EconomyManager.Instance.AddMoney(item.price);
        }
        else
        {
            Debug.Log("Cannot decrement below zero.");
        }
    }



//     void UpdateStarRating(float rating)
        // {
        //     // Reset semua bintang
        //     foreach (var star in starIcons)
        //     {
        //         star.SetActive(false);
        //     }

        //     // Aktifkan bintang sesuai rating
        //     for (int i = 0; i < Mathf.FloorToInt(rating); i++)
        //     {
        //         if(i < starIcons.Length) starIcons[i].SetActive(true);
        //     }

        //     // Handle setengah bintang
        //     if (rating % 1 >= 0.5f)
        //     {
        //         int halfStarIndex = Mathf.FloorToInt(rating);
        //         if(halfStarIndex < starIcons.Length) 
        //             starIcons[halfStarIndex].SetActive(true);
        //     }
        // }
    }