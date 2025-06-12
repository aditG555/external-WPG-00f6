using System.Xml.Serialization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ShelveItme : DragAndDrop,IDropHandler
{
    [SerializeField] UnityEvent End;
    public GameObject Item;
    public GameObject DraggedItem;
    public GameObject Parrent;
    public int Count;
    public bool isInfinite = false;
    TMPro.TextMeshProUGUI Label;
    public TextMeshProUGUI TextLabel;
    DragAndDrop drag;
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        //Root = canvas.transform;
        //Parrent = Root.gameObject;
        
    }
    private void Start()
    {
        Label = this.gameObject.transform.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        TextLabel.text = this.name;
        
        UpdateCount();
    }
    public override void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("StartTaking");
        if(Count > 0 || isInfinite)
        {
            DraggedItem = Instantiate(Item, canvas.transform, Parrent);
            drag = DraggedItem.GetComponent<DragAndDrop>();
            drag.GetComponent<RectTransform>().position = eventData.position;
            //drag.ParentBeforeDrag = this.transform;
            drag.Set(this.canvas);
            drag.enabled = true;
            drag.canvasGroup.blocksRaycasts = false;
            drag.ShelveFrom = this;
            eventData.pointerDrag = DraggedItem;
            End.Invoke();
        }
        
        //DraggedItem.transform.position = Input.mousePosition/ DraggedItem.canvas.scaleFactor;
    }
    public void UpdateText(int text)
    {
        Label.text = text.ToString() + "X";
    }
    public void AddCount(int val)
    {
        if (!isInfinite)
        {
            Count += val;
            UpdateText(Count);
        }
    }
    public void UpdateCount()
    {
        if (!isInfinite)
        {
            UpdateText(Count);
        }
    }


    public void MinCount(int val)
    {
        if (!isInfinite)
        {
            Count -= val;
            UpdateText(Count);
        }
    }
    public void IncrementCount()
    {
        if (!isInfinite)
        {
            Count++;
            UpdateText(Count);
        }
    }
    public void DencrementCount()
    {
        if (!isInfinite)
        {
            Count--;
            UpdateText(Count);
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag.GetComponent<DragAndDrop>().itemType == Item.GetComponent<DragAndDrop>().itemType)
        {
            IncrementCount();
            eventData.pointerDrag.GetComponent<DragAndDrop>().InsertInto(this.gameObject, true);
        }
    }
    public override void OnDrag(PointerEventData eventData)
    {
        //DraggedItem.rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("EndDragging");
        
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("PointerDown");
    }

}
