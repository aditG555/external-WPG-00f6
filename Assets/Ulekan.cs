using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Ulekan : MonoBehaviour, IPointerDownHandler, IDropHandler
{
    [SerializeField] UnityEvent Activate;
    public void OnPointerDown(PointerEventData eventData)
    {
    }
    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag.GetComponent<DragAndDrop>() != null)
        {
            Activate.Invoke();
        }
    }
}
