using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CuttingBoardIcon : MonoBehaviour, IPointerDownHandler, IDropHandler
{
    [SerializeField] UnityEvent Activate;
    [SerializeField] CuttingInteraction interaction;

    [Header("Item Ouput List")]
    [Space(10)]
    [SerializeField] GameObject JaheOut;
    [SerializeField] GameObject KencurOut;
    [SerializeField] GameObject TemulawakOut;
    [SerializeField] GameObject KunyitOut;
    [SerializeField] GameObject SeraiOut;
    Sprite dropedItemSprite;
    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.GetComponent<DragAndDrop>() != null)
        {
            Debug.Log("Ulekan Just Got Dropepd something");
            if (eventData.pointerDrag.GetComponent<DragAndDrop>().canbeCut)
            {
                DragAndDrop item = eventData.pointerDrag.GetComponent<DragAndDrop>();
                interaction.UnCutItemSprite = eventData.pointerDrag.GetComponent<Image>().sprite;
                switch (eventData.pointerDrag.GetComponent<DragAndDrop>().itemType)
                {
                    case ItemType.Jahe:
                        interaction.ItemOutput = JaheOut;
                        break;
                    case ItemType.Kencur:
                        interaction.ItemOutput = KencurOut;
                        break;
                    case ItemType.Temulawak:
                        interaction.ItemOutput = TemulawakOut;
                        break;
                    case ItemType.Serai:
                        interaction.ItemOutput = SeraiOut;
                        break;
                    case ItemType.Kunyit:
                        interaction.ItemOutput = KunyitOut;
                        break;
                    default:
                        interaction.ItemOutput = JaheOut;
                        break;
                }
                if (GameManager._instance.isFilled())
                {
                    Debug.Log("Hand is Not Full!");
                    //eventData.pointerDrag.GetComponent<DragAndDrop>().ShelveFrom?.DencrementCount();
                    //Destroy(eventData.pointerDrag);
                    item.InsertInto(this.gameObject, true);
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

