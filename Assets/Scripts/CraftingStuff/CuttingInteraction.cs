using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;
using NUnit;
using static UnityEditor.Progress;
using System.Threading;

public class CuttingInteraction : MonoBehaviour
{
    public Canvas canvas;
    public GameObject collisionCheck;
    public float progress;
    public Sprite UnCutItemSprite;
    public GameObject ItemOutput;
    public Image Itemcut;
    public Button ItemOut;
    public float SliderOfsets;
    [SerializeField] Slider slider;
    [SerializeField] UnityEvent Dispense;
    RectTransform rectTransform;
    CanvasGroup canvasGroup;
    Vector2 startposition;
    float MaxProgressValue = 5f;
    private void Awake()
    {
        gameObject.SetActive(false);
        canvasGroup = GetComponent<CanvasGroup>();
        
    }
    private void OnEnable()
    {
        Itemcut.sprite = UnCutItemSprite;
    }
    private void Start()
    {
        startposition = slider.GetComponent<RectTransform>().anchoredPosition;
    }

    // Update is called once per frame
    bool EYe = true;
    
   
    void Update()
    {
        
        if(progress < MaxProgressValue)
        {
            slider.interactable = true;
            if (slider.value == 1)
            {
                if (EYe)
                {
                    
                    progress++;
                    EYe = false;
                }
                slider.value = 0f;
                slider.interactable = false;
                slider.GetComponent<RectTransform>().anchoredPosition = slider.GetComponent<RectTransform>().anchoredPosition + new Vector2(1 * SliderOfsets, 0);
            }
            else if(slider.value >= 0.01f)
            {
                EYe = true;
            }
            
        }
        else
        {
            Dispense.Invoke();
        }
        
    }

    public void ResetObject()
    {
        ResetSliderVal();
        slider.GetComponent<RectTransform>().anchoredPosition = startposition;
    }
    public void ResetSliderVal()
    {
        gameObject.SetActive(false);
        progress = 0;
        slider.value = 0f;
        Debug.Log("Reset Slider");
    }
    public void OutPut()
    {
        ItemOut.gameObject.SetActive(false );
        GameObject OUtput = GameObject.Instantiate(ItemOutput, GameObject.Find("HandItem").transform);
        if (GameManager._instance.AddItemInHand(OUtput))
        {
            ResetObject();
        }
        else
        {
            Debug.Log("HandIsFull");
            ResetObject();
            Destroy(OUtput);
        }
    }
}
