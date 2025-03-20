using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler,IEndDragHandler,IDragHandler
{
    public Transform ParentAfterDrag;
    [HideInInspector] public RectTransform rectTransform;
    [HideInInspector] public CanvasGroup canvasGroup;
    [SerializeField] public Canvas canvas;
    protected Transform Root;
    Transform ParentBeforeDrag;
    public void Set(Canvas canvas)
    {
        this.canvas = canvas;
    }
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        Root = canvas.transform;
        ParentBeforeDrag = this.transform.parent.transform;
    }
    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("StartDragging");
        ParentBeforeDrag = ParentAfterDrag;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.5f;
        transform.SetParent(canvas.transform);
        transform.SetAsLastSibling();
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition  += eventData.delta / canvas.scaleFactor;
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("EndDragging");
        
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;

        if (ParentAfterDrag != ParentBeforeDrag)
        {
            transform.SetParent(ParentAfterDrag);
            transform.position = ParentAfterDrag.position;
        }
        else if(ParentBeforeDrag != null)
        {
            transform.SetParent(ParentBeforeDrag);
            transform.position = ParentBeforeDrag.position;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("PointerDown");
    }
}
