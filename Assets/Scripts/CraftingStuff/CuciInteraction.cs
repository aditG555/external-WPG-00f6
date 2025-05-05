using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class CuciInteraction : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Canvas canvas;
    public GameObject collisionCheck;
    public float progress;
    public UlekanOutput UlOut;
    public GameObject ItemOutput;
    public DirtyItem dirtyItem;
    [SerializeField] UnityEvent Berak;
    RectTransform rectTransform;
    CanvasGroup canvasGroup;
    Vector2 startposition;
    Image DirtyImg;
    Image CleanImg;
    float MaxProgressValue = 5000f;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        
    }
    void OnEnable()
    {
        DirtyImg.sprite = dirtyItem.DirtySprite;
        CleanImg.sprite = dirtyItem.CleanSprite;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("StartDragging");
        startposition = this.rectTransform.position;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        if (eventData.hovered.Contains(collisionCheck))
        {
            progress += eventData.delta.magnitude;
        }
        if (progress > MaxProgressValue)
        {
            OnEndDrag(eventData);
            UlOut.SetGameObject(ItemOutput);
            Berak.Invoke();
            return;
        }
        //Debug.Log(progress);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        this.rectTransform.position = startposition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("PointerDown");
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void ResetVal()
    {
        progress = 0;
    }

}
