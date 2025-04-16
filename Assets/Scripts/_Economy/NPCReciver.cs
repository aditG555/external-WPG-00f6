using UnityEngine;
using UnityEngine.EventSystems;

public class NPCReceiver : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject jamuItem = eventData.pointerDrag;
        GetComponent<NPC>().HandleItemDrop(jamuItem);
        Destroy(jamuItem); // Hancurkan item setelah digunakan
    }
}