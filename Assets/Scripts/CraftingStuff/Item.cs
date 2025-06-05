using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Jamu Game/Items")]
public class Item : MonoBehaviour
{
    public Sprite ItemSprite;
    public ItemType ItemType;
    public Jamu JamuComponent;
    public bool CanBeClean;
    public bool CanBeCut;
    public bool CanBeBoil;
    private void Awake()
    {
        JamuComponent = gameObject.GetComponent<Jamu>();
        gameObject.GetComponent<Image>().sprite = ItemSprite;
    }
}
