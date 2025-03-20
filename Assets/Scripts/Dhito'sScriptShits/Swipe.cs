using UnityEngine;
using UnityEngine.EventSystems;

public class Swipe : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] Transform Root;
    [SerializeField] GameObject Prev;
    [SerializeField] GameObject Next;
    Camera _camera;
    GameManager gameManager;
    public float SwipeFactor;
    private void Awake()
    {
        gameManager = GameManager.GetInstance();
        SwipeFactor = gameManager.SceneSwipeFactor;
        _camera = Camera.main;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("StartDragging");
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.delta.x > 1)
        {
            SwipeR();
        }
        if(eventData.delta.x < -1)
        {
            SwipeL();
        }
    }
    public void SwipeR()
    {
        Debug.Log("Right");
        if (Prev != null)
        {
            Root.position += new Vector3(SwipeFactor, 0f, 0f);
        }
    }
    public void SwipeL()
    {
        Debug.Log("Left");
        if(Next != null)
        {
            Root.position -= new Vector3(SwipeFactor, 0f, 0f);
        }
    }
}

