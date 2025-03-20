using UnityEngine;
using UnityEngine.EventSystems;

public class CraftingSlot : MonoBehaviour, IDropHandler
{
    public string itemName = "";
    public int index;
    
    [Header("References")]
    public CraftingManager craftingManager; 

    public void SetItem(string itemName)
    {
        this.itemName = itemName;
    }
    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedItem = eventData.pointerDrag;
        ItemIdentifier item = droppedItem.GetComponent<ItemIdentifier>();
        
        if (item != null)
        {
            itemName = item.itemName;
            craftingManager.CheckRecipe();
        }
    }

    public void ClearSlot()
    {
        itemName = "";
    }
}