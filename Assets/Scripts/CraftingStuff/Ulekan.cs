using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Ulekan : MonoBehaviour, IPointerDownHandler, IDropHandler
{
    [SerializeField] UnityEvent Activate;
    [SerializeField] UlekanInteraction interaction;

    [Header("Item Ouput List")]
    [Space(10)]
    [SerializeField] GameObject BroWhatOut;
    [SerializeField] GameObject JaheOut;
    [SerializeField] GameObject KencurOut;
    [SerializeField] GameObject TemulawakOut;
    [SerializeField] GameObject KunyitOut;
    [SerializeField] GameObject SeraiOut;

    public void OnPointerDown(PointerEventData eventData)
    {
    }
    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag.GetComponent<DragAndDrop>() != null)
        {
            Debug.Log("Ulekan Just Got Dropepd something");
            if (eventData.pointerDrag.GetComponent<DragAndDrop>().canbeUllek)
            {
                Debug.Log("ello");
                DragAndDrop item = eventData.pointerDrag.GetComponent<DragAndDrop>();
                //if(eventData.pointerDrag.GetComponent<DragAndDrop>().itemType == ItemType.)
                switch (eventData.pointerDrag.GetComponent<DragAndDrop>().itemType)
                {
                    case ItemType.Air:
                        interaction.ItemOutput = BroWhatOut;
                        break;
                    case ItemType.Jahe:
                        interaction.ItemOutput = JaheOut;
                        break;
                    default:
                        interaction.ItemOutput = BroWhatOut;
                        break;
                }
                if (GameManager._instance.isFilled())
                {
                    Debug.Log("Hand is Not Full!");
                    eventData.pointerDrag.GetComponent<DragAndDrop>().ShelveFrom.DencrementCount();
                    Destroy(eventData.pointerDrag);
                    Activate.Invoke();
                }
                else
                {
                    Debug.Log("Hand is Full!");
                }
                //eventData.pointerDrag.GetComponent<DragAndDrop>().ParentBeforeDrag = null;
            }
            
        }
    }
}
