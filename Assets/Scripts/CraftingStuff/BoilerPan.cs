using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
    [SerializeField] GameObject BerasKencurOut;
    [SerializeField] GameObject TehChamomilleOut;
    [SerializeField] GameObject TehRossellaOut;
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
        if (eventData.pointerDrag.GetComponent<Item>() != null)
        {
            if (eventData.pointerDrag.GetComponent<Item>().CanBeBoil)
            {
                if(Recepy.Count <= 11)
                {
                    DragAndDrop item = eventData.pointerDrag.GetComponent<DragAndDrop>();
                    //if(eventData.pointerDrag.GetComponent<DragAndDrop>().itemType == ItemType.)
                    item.ParentAfterDrag = ItemsGroup.transform;
                    item.canbeDrag = false;
                    Recepy.Add(item.itemType);
                    //Beras Kencur
                    if (Recepy.Contains(ItemType.Air))
                    {
                        Debug.Log("HasWater!");
                        if (Recepy.Contains(ItemType.Beras))
                        {
                            if (Recepy.Contains(ItemType.Jahe))
                            {
                                if (Recepy.Contains(ItemType.DaunPandan))
                                {
                                    if (Recepy.Contains(ItemType.GulaAren))
                                    {
                                        if (Recepy.Contains(ItemType.GulaPasir))
                                        {
                                            if (Recepy.Contains(ItemType.Kencur))
                                            {
                                                if (Recepy.Contains(ItemType.Garam))
                                                {
                                                    OutPut = BerasKencurOut;
                                                }
                                                else
                                                {
                                                    OutPut = null;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        //Temulawak Logic
                        if (Recepy.Contains(ItemType.Temulawak))
                        {
                            if (Recepy.Contains(ItemType.Kunyit))
                            {
                                if (Recepy.Contains(ItemType.Kencur))
                                {
                                    if (Recepy.Contains(ItemType.Serai))
                                    {
                                        if (Recepy.Contains(ItemType.GulaAren))
                                        {
                                            if (Recepy.Contains(ItemType.Garam))
                                            {
                                                OutPut = TemulawakOut;
                                            }
                                            else
                                            {
                                                OutPut = null;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        //Kunyit Logic
                        if (Recepy.Contains(ItemType.Kunyit))
                        {
                            if (Recepy.Contains(ItemType.Serai))
                            {
                                if (Recepy.Contains(ItemType.AsamJawa))
                                {
                                    if (Recepy.Contains(ItemType.JerukNipis))
                                    {
                                        if (Recepy.Contains(ItemType.GulaAren))
                                        {
                                            OutPut = TemulawakOut;
                                        }
                                        {
                                            OutPut = null;
                                        }
                                    }
                                }
                            }
                        }
                        //Teh Rossella Logic
                        if (Recepy.Contains(ItemType.KelopakRosela))
                        {
                            if (Recepy.Contains(ItemType.GulaPasir))
                            {
                                OutPut = TehRossellaOut;
                                RebusImg.color = Color.green;
                            }
                            else
                            {
                                OutPut = null;
                            }
                        }
                        //Teh Chamomile Logic
                        if (Recepy.Contains(ItemType.KelopakChamomile))
                        {
                            if (Recepy.Contains(ItemType.GulaPasir))
                            {
                                if (Recepy.Contains(ItemType.JerukNipis))
                                {
                                    OutPut = TehChamomilleOut;
                                    RebusImg.color = Color.white;
                                }
                                else
                                {
                                    OutPut = null;
                                }
                            }
                        }
                        //Teh Adas
                        if (Recepy.Contains(ItemType.BijiAdas))
                        {
                            Debug.Log("HasBijiAdas!");
                            if (Recepy.Contains(ItemType.GulaPasir))
                            {
                                Debug.Log("HasGulaPasir!");
                                OutPut = TehAdasOut;
                                RebusImg.color = Color.red;

                            }
                            else
                            {
                                OutPut = null;
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
