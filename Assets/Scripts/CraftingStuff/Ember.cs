using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Ember : MonoBehaviour, IPointerDownHandler, IDropHandler
{
    [SerializeField] UnityEvent Activate;
    [SerializeField] CuciInteraction interaction;

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
            if (eventData.pointerDrag.GetComponent<DirtyItem>() != null)
            {
                DragAndDrop item = eventData.pointerDrag.GetComponent<DragAndDrop>();

                interaction.dirtyItem = item.GetComponent<DirtyItem>();

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
                    item.InsertInto(this.gameObject, true);
                    Activate.Invoke();
                }
                else
                {
                    Debug.Log("Hand is Full!");
                }
            }
            
        }
    }
}
