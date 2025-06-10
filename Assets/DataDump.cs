using UnityEngine;

public class DataDump : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    GameManager gameManager;
    [SerializeField] int ItemAmount;
    void Start()
    {
        gameManager = GameManager._instance;
        foreach(ShelveItme shelve in gameManager.Shelve)
        {
            shelve.AddCount(ItemAmount);
        }
    }
}
