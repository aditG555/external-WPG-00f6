using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    DragAndDrop dragAndDropItem;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Droped to Slot");
        if(eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.GetComponent<ShelveItme>() != null)
            {
                eventData.pointerDrag.GetComponent<ShelveItme>().DraggedItem.GetComponent<DragAndDrop>().SetParrent(transform);
            }
            else if(eventData.pointerDrag.GetComponent<DragAndDrop>() != null)
            {
                eventData.pointerDrag.GetComponent<DragAndDrop>().SetParrent(transform);
            }
            
        }
    }
}