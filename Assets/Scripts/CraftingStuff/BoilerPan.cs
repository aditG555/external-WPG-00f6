using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BoilerPan : MonoBehaviour, IPointerDownHandler, IDropHandler
{
    public GameObject ItemsGroup;
    public List<GameObject> Recepy;

    [SerializeField] UnityEvent Activate;

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
        if (eventData.pointerDrag.GetComponent<DragAndDrop>() != null)
        {
            if (eventData.pointerDrag.GetComponent<DragAndDrop>().canbeBoil)
            {
                DragAndDrop item = eventData.pointerDrag.GetComponent<DragAndDrop>();
                //if(eventData.pointerDrag.GetComponent<DragAndDrop>().itemType == ItemType.)
                item.ParentAfterDrag = ItemsGroup.transform;
                item.canbeDrag = false;
                Recepy.Add(item.gameObject);
                switch (eventData.pointerDrag.GetComponent<DragAndDrop>().itemType)
                {
                    case ItemType.Air:
                        break;
                    case ItemType.Jahe:
                        break;
                    default:

                        break;
                }
                if (!GameManager._instance.isFilled())
                {
                    eventData.pointerDrag.GetComponent<DragAndDrop>().ShelveFrom.DencrementCount();
                    Destroy(eventData.pointerDrag);
                    Activate.Invoke();
                }
            }

        }
    }
}
