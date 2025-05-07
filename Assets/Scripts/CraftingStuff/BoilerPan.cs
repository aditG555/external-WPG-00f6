using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class BoilerPan : MonoBehaviour, IPointerDownHandler, IDropHandler
{
    public GameObject ItemsGroup;
    public List<ItemType> Recepy;
    public GameObject OutPut;

    [SerializeField] UnityEvent Activate;
    [SerializeField] UnityEvent End;

    [Header("Item Ouput List")]
    [Space(10)]
    [SerializeField] GameObject BroWhatOut;
    [SerializeField] GameObject JaheOut;
    [SerializeField] GameObject TehAdasOut;
    [SerializeField] GameObject TemulawakOut;
    [SerializeField] GameObject KunyitOut;
    [SerializeField] GameObject SeraiOut;
    [Space(10)]
    //[Header("Item Input List")]
    //[Space(10)]
    //[SerializeField] GameObject Water;
    //[SerializeField] GameObject Jahe;
    //[SerializeField] GameObject Kencur;
    //[SerializeField] GameObject Temulawak;
    //[SerializeField] GameObject Kunyit;
    //[SerializeField] GameObject Serai;
    //[SerializeField] GameObject BijiAdas;
    //[SerializeField] GameObject GulaPasir;
    Image RebusImg;
    Color originalColor;
    private void Awake()
    {
        RebusImg = GetComponent<Image>();
        End.AddListener(ClearRecepy);
        originalColor = RebusImg.color;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if(OutPut != null)
        {
            GameObject OUtput = GameObject.Instantiate(OutPut, GameObject.Find("HandItem").transform);
            if (GameManager._instance.AddItemInHand(OUtput))
            {
                End.Invoke();
            }
            else
            {
                Debug.Log("HandIsFull");
                End.Invoke();
                Destroy(OUtput);
            }
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.GetComponent<DragAndDrop>() != null)
        {
            if (eventData.pointerDrag.GetComponent<DragAndDrop>().canbeBoil)
            {
                if(Recepy.Count <= 11)
                {
                    DragAndDrop item = eventData.pointerDrag.GetComponent<DragAndDrop>();
                    //if(eventData.pointerDrag.GetComponent<DragAndDrop>().itemType == ItemType.)
                    item.ParentAfterDrag = ItemsGroup.transform;
                    item.canbeDrag = false;
                    Recepy.Add(item.itemType);
                    if (Recepy.Contains(ItemType.Air))
                    {
                        Debug.Log("HasWater!");
                        if (Recepy.Contains(ItemType.BijiAdas))
                        {
                            Debug.Log("HasBijiAdas!");
                            if (Recepy.Contains(ItemType.GulaPasir))
                            {
                                Debug.Log("HasGulaPasir!");
                                OutPut = TehAdasOut;
                                RebusImg.color = Color.red;

                            }
                        }
                    }
                    if (!GameManager._instance.isFilled())
                    {
                        //eventData.pointerDrag.GetComponent<DragAndDrop>().ShelveFrom.DencrementCount();
                        Destroy(eventData.pointerDrag);
                        Activate.Invoke();
                    }
                }
                
            }

        }
    }
    public void ClearRecepy()
    {
        Recepy.Clear();
        foreach (Transform child in ItemsGroup.transform)
        {
            Destroy(child.gameObject);
        }
        RebusImg.color = originalColor;
    }
}
