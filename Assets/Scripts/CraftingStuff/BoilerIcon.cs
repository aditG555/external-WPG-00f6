using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BoilerIcon : MonoBehaviour, IPointerDownHandler
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
        Activate.Invoke();
    }
    //public void OnDrop(PointerEventData eventData)
    //{
    //    if (eventData.pointerDrag.GetComponent<DragAndDrop>() != null)
    //    {
    //        if (eventData.pointerDrag.GetComponent<DragAndDrop>().canbeBoil)
    //        {
    //            //if(eventData.pointerDrag.GetComponent<DragAndDrop>().itemType == ItemType.)
    //            switch (eventData.pointerDrag.GetComponent<DragAndDrop>().itemType)
    //            {
    //                case ItemType.Air:
    //                    interaction.ItemOutput = BroWhatOut;
    //                    break;
    //                case ItemType.Jahe:
    //                    interaction.ItemOutput = JaheOut;
    //                    break;
    //                default:
    //                    interaction.ItemOutput = BroWhatOut;
    //                    break;
    //            }
    //            if (GameManager._instance.isFilled())
    //            {
    //                eventData.pointerDrag.GetComponent<DragAndDrop>().ShelveFrom.DencrementCount();
    //                Destroy(eventData.pointerDrag);
                    
    //            }
    //        }

    //    }
    //}
}

