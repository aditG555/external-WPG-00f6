using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class GameManager : MonoBehaviour
{
    [SerializeField] public UIDocument UIDocument;
    public float SceneSwipeFactor = 900;
    public static GameManager _instance;
    public List<ItemSlot> Slots;
    public bool InputAllowed = true;
    public bool AddItemInHand(GameObject item)
    {
        foreach (ItemSlot slot in Slots)
        {
            if (!slot.IsFilled())
            {
                Debug.Log(slot.IsFilled());
                item.GetComponent<DragAndDrop>().ParentAfterDrag = slot.transform;
                slot.SetChild(item);
                item.GetComponent<DragAndDrop>().transform.position = item.GetComponent<DragAndDrop>().ParentAfterDrag.position;
                return true;
            }
        }
        return false;
    }
    public bool isFilled()
    {
        foreach (ItemSlot slot in Slots)
        {
            if (slot.IsFilled())
            {
                continue;
            }
            else
            {
                return true;
            }
        }
        Debug.Log("Hand Is Full");
        return false;
    }
    public static GameManager GetInstance()
    {
        if (_instance == null)
        {
            //_instance = new GameObject("_GameManager").AddComponent<GameManager>();
        }

        return _instance;
    }
    public static float GetSceneSwipeFactor()
    {
        return GetInstance().SceneSwipeFactor;
    }


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if(_instance != this)
        {
            Destroy(this);
        }
        Debug.Log("GM Start() is Triggerde!");
        LoadData();
        EconomyManager.Instance.UpdateMoneyUI();
        //DontDestroyOnLoad(this);

    }
    public void SaveData()
    {
        SaveSystem.SaveData(this,EconomyManager.Instance,DayCycleManager.Instance);
        //Debug.Log(EconomyManager.Instance.Popularity);
        //Debug.Log(EconomyManager.Instance.currentMoney);
        Debug.Log("Data Saved");
    }
    public void LoadData()
    {
        SaveData data = SaveSystem.LoadData();
        if(data != null)
        {
            for (int i = 0; i < Shelve.Length; i++)
            {
                Shelve[i].Count = data.InventoryItems[i];
            }
            EconomyManager.Instance.Popularity = data.Popularity;
            EconomyManager.currentMoney = data.Money;
            DayCycleManager.Instance.currentDay = data.Day;
            Debug.Log(data.Popularity);
            Debug.Log(data.Money);
            Debug.Log(data.Day);
        }
        Debug.Log("Data Loaded");
        
    }
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
