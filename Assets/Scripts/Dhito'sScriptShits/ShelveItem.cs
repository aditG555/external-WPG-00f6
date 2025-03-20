using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ShelveItme : DragAndDrop
{
    [SerializeField] UnityEvent End;
    public GameObject Item;
    public GameObject DraggedItem;
    public GameObject Parrent;
    DragAndDrop drag;
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        Root = canvas.transform;
        Parrent = Root.gameObject;
    }
    public override void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("StartTaking");
        DraggedItem = Instantiate(Item, canvas.transform,Parrent);
        drag = DraggedItem.GetComponent<DragAndDrop>();
        drag.GetComponent<RectTransform>().position = eventData.position;
        drag.Set(this.canvas);
        drag.enabled = true;
        drag.canvasGroup.blocksRaycasts = false;
        eventData.pointerDrag = DraggedItem;
        End.Invoke();
        //DraggedItem.transform.position = Input.mousePosition/ DraggedItem.canvas.scaleFactor;
    }

    public override void OnDrag(PointerEventData eventData)
    {
        //DraggedItem.rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("EndDragging");
        
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("PointerDown");
    }
}
