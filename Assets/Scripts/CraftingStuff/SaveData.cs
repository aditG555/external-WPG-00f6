using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int Day;
    public int Money;
    public int Popularity;
    public SaveData(EconomyManager economy, DayCycleManager day)
    {
        Money = economy.currentMoney;
    }
}
