using UnityEngine;
using UnityEngine.EventSystems;

public class NPCReceiver : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag.GetComponent<DragAndDrop>() != null)
        {
            if(eventData.pointerDrag.GetComponent<ShelveItme>() != null) return;
            GameObject jamuItem = eventData.pointerDrag;
            GetComponent<NPC>().HandleItemDrop(jamuItem);
            Destroy(jamuItem); // Hancurkan item setelah digunakan
        }
    }
}