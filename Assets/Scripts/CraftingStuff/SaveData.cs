using JetBrains.Annotations;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int Day;
    public int Money;
    public int Popularity;
    public int[] InventoryItems;
    public SaveData(EconomyManager economy, DayCycleManager day,GameManager gameManager)
    {
        InventoryItems = new int[gameManager.Shelve.Length];
        for(int i = 0; i < InventoryItems.Length; i++)
        {
            InventoryItems[i] = gameManager.Shelve[i].Count;
        }
        Money = economy.currentMoney;
        Popularity = economy.Popularity;

    }
}
