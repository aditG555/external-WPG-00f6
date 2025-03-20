using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class UlekanInteraction : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] Slider slider;
    [SerializeField] UnityEvent Berak;
    public Canvas canvas;
    public GameObject collisionCheck;
    RectTransform rectTransform;
    CanvasGroup canvasGroup;
    public float progress;
    Vector2 startposition;
    float MaxProgressValue = 5000f;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        
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
            Berak.Invoke();
        }
        Debug.Log(progress);
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
        slider.value = progress/MaxProgressValue;
    }
    public void Show()
    {
        canvas.gameObject.SetActive(true);
    }

}
