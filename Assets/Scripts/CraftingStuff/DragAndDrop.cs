using System;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler,IEndDragHandler,IDragHandler
{
    [HideInInspector] public RectTransform rectTransform;
    [HideInInspector] public CanvasGroup canvasGroup;
    [SerializeField] public Canvas canvas;
    public bool canbeDrag = true;
    public bool canbeUllek = true;
    public bool canbeBoil = true;
    public bool canbeCut = true;
    public int MaxItemStack = 10;
    public ItemType itemType;
    public Transform ParentAfterDrag;
    public Transform ParentBeforeDrag;
    public ShelveItme ShelveFrom;
    bool killafter = false;
    protected Transform Root;

    
    public void Set(Canvas canvas)
    {
        this.canvas = canvas;
    }
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }
    private void Start()
    {
        //if(ParentBeforeDrag == null)
        //{
        //    ParentBeforeDrag = transform.parent.transform;
        //}
        
    }
    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        //ParentBeforeDrag = ParentAfterDrag;
        if (canbeDrag)
        {
            ParentBeforeDrag.gameObject.GetComponent<ItemSlot>()?.RemoveItem(this.gameObject);
            ParentAfterDrag = null;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0.5f;
            canvas = GameObject.Find("InventoryTab")?.GetComponent<Canvas>();
            transform.SetParent(canvas.transform);
            transform.SetAsLastSibling();
        }
        
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        if (canbeDrag)
        {
            if(canvas == null) canvas = GameObject.Find("InventoryTab").GetComponent<Canvas>();
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }
        

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        if (canbeDrag)
        {
            Debug.Log("EndDragging");

            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1f;

            InsertToSlot();

            //if(ParentAfterDrag == false)
            //{
            //    Destroy(gameObject);
            //}
        }else if (ParentAfterDrag != ParentBeforeDrag)
        {
            InsertToSlot();
        }


    }
    public void InsertToSlot()
    {
        if (ParentAfterDrag != ParentBeforeDrag)
        {
            Debug.Log("1");
            if (ParentAfterDrag != null)
            {
                transform.SetParent(ParentAfterDrag);
                transform.position = ParentAfterDrag.position;
                ParentBeforeDrag = ParentAfterDrag;
                ParentAfterDrag = null;
            }
            else
            {
                Debug.Log("21");
                if (killafter) Destroy(gameObject);
                transform.SetParent(ParentBeforeDrag);
                transform.position = ParentBeforeDrag.position;
            }
            if(ShelveFrom != null)
            {
                Debug.Log("Shelve Decrement");
                ShelveFrom.DencrementCount();
                ShelveFrom = null;
                //if(ParentAfterDrag == null)
                //{
                //    Destroy(gameObject);
                //}
            }
            if (killafter) Destroy(gameObject);
        }
        else if (ParentAfterDrag == null)
        {
            Debug.Log("2");
            if (killafter)
            {
                ShelveFrom?.DencrementCount();
                Destroy(gameObject);
            }
            transform.SetParent(ParentBeforeDrag);
            transform.position = ParentBeforeDrag.position;
        }
        else
        {
            Debug.Log("3");
            if (killafter)
            {
                ShelveFrom.DencrementCount();
            }
            Destroy(gameObject);
        }
        //if (killafter)
        //{
        //    ShelveFrom.DencrementCount();
        //    Destroy(gameObject);
        //}
    }
    //public void InsertToSlot(Transform parent)
    //{
    //    ParentAfterDrag = parent;
    //    transform.SetParent(ParentAfterDrag);
    //    transform.position = ParentAfterDrag.position;
    //} Unused
    public void InsertInto(GameObject parrentS)
    {

        ParentAfterDrag = parrentS.transform;

    }
    public void InsertInto(GameObject parrentS, bool isDestroy)
    {

        ParentAfterDrag = parrentS.transform;
        killafter = isDestroy;

    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("PointerDown");
    }
}
public enum ItemType
{
    Air,
    Jahe,
    GulaPasir,
    GulaAren,
    Garam,
    DaunPandan,
    Beras,
    Kencur,
    Temulawak,
    Kunyit,
    Serai,
    AsamJawa,
    JerukNipis,
    KelopakRosela,
    BijiAdas,
    KelopakChamomile

}
