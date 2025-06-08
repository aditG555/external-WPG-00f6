using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class UlekanInteraction : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Canvas canvas;
    public GameObject collisionCheck;
    public float progress;
    public UlekanOutput UlOut;
    public GameObject ItemOutput;
    [SerializeField] Slider slider;
    [SerializeField] UnityEvent Berak;
    RectTransform rectTransform;
    CanvasGroup canvasGroup;
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
            UlOut.SetGameObject(ItemOutput);
            UlOut.Image.sprite = ItemOutput.GetComponent<Image>().sprite;
            Berak.Invoke();
            return;
        }
        slider.value = Mathf.Clamp01(progress/MaxProgressValue);
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
    public void ResetSliderVal()
    {
        progress = 0;
        slider.value = 0f;
        Debug.Log("Reset Slider");
    }

}
