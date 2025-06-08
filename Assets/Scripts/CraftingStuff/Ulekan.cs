using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Ulekan : MonoBehaviour, IDropHandler
{
    [SerializeField] UnityEvent Activate;
    [SerializeField] UlekanInteraction interaction;

    [Header("Item Ouput List")]
    [Space(10)]
    [SerializeField] GameObject BroWhatOut;
    [SerializeField] GameObject JaheOut;
    [SerializeField] GameObject KencurOut;
    [SerializeField] GameObject DaunpandanOut;
    [SerializeField] GameObject TemulawakOut;
    [SerializeField] GameObject KunyitOut;
    [SerializeField] GameObject SeraiOut;
    [SerializeField] GameObject RossellaOut;
    public bool Ulek;
    public bool Cuci;

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag.GetComponent<DragAndDrop>() != null)
        {
            Debug.Log("Ulekan Just Got Dropepd something");
            if (eventData.pointerDrag.GetComponent<DragAndDrop>().canbeUllek == Ulek || eventData.pointerDrag.GetComponent<Item>().CanBeClean == Cuci)
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
                    case ItemType.DaunPandan:
                        interaction.ItemOutput = DaunpandanOut;
                        break;
                    case ItemType.Kencur:
                        interaction.ItemOutput = KencurOut;
                        break;
                    case ItemType.Temulawak:
                        interaction.ItemOutput = TemulawakOut;
                        break;
                    case ItemType.Kunyit:
                        interaction.ItemOutput = KunyitOut;
                        break;
                    case ItemType.Serai:
                        interaction.ItemOutput = SeraiOut;
                        break;
                    case ItemType.KelopakRosela:
                        interaction.ItemOutput = RossellaOut;
                        break;
                    default:
                        interaction.ItemOutput = BroWhatOut;
                        break;
                }
                interaction.transform.GetComponent<Image>().sprite = item.transform.GetComponent <Image>().sprite;
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
