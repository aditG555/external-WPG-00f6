using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    DragAndDrop dragAndDropItem;
    //[SerializeField] GameObject Child;
    [SerializeField] List<GameObject> Child = new List<GameObject>();
    public bool canbeDrop = true;
    int ItemCount = 0;
    public bool IsFilled()
    {
        if(Child.Count != 0)
        {
            if (Child.Count < Child[0].GetComponent<DragAndDrop>().MaxItemStack)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return false;
        }
        
    }
    public void RemoveItem(GameObject item)
    {
        Child.Remove(item);
    }
    private void Start()
    {
        //Debug.Log(IsFilled());
    }
    public void OnDrop(PointerEventData eventData)
    {
        
        if(canbeDrop){
            Debug.Log("Droped to Slot");
            if (eventData.pointerDrag != null)
            {
                if (eventData.pointerDrag.GetComponent<ShelveItme>() != null)
                {
                    eventData.pointerDrag.GetComponent<ShelveItme>().DraggedItem.GetComponent<DragAndDrop>().SetParrent(transform);

                }
                else if (eventData.pointerDrag.GetComponent<DragAndDrop>() != null)
                {
                    //Child = eventData.pointerDrag;
                    //eventData.pointerDrag.GetComponent<DragAndDrop>().SetParrent(transform);
                    //eventData.pointerDrag.GetComponent<DragAndDrop>().ParentAfterDrag = transform;
                    eventData.pointerDrag.GetComponent<DragAndDrop>().InsertInto(this.gameObject);
                    //SetChild(eventData.pointerDrag);
                }

            }
        }
    }
    public void SetChild(GameObject Item)
    {
        Child.Add(Item);
        Item.GetComponent<DragAndDrop>().ParentAfterDrag = transform;
        Item.transform.SetParent(transform);
        ItemCount++;
    }
}