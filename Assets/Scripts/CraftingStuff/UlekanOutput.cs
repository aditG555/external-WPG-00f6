using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UlekanOutput : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] UnityEvent End;
    public GameObject Item;
    public Image Image;
    public GameObject DraggedItem;
    public GameObject Parrent;
    public void SetGameObject(GameObject output)
    {
        Item = output;
        Image = transform.GetComponent<Image>();
    }
    

    public void OnPointerDown(PointerEventData eventData)
    {
        
        GameObject OUtput = GameObject.Instantiate(Item,GameObject.Find("HandItem").transform);
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
