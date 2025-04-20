using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.XR;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;
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
                //item.GetComponent<DragAndDrop>().ParentAfterDrag = slot.transform;
                item.GetComponent<DragAndDrop>().InsertInto(slot.gameObject);
                //slot.SetChild(item);
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
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
