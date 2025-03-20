using UnityEngine;
using UnityEngine.EventSystems;

public class NPCReceiver : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject draggedItem = eventData.pointerDrag;
        string itemName = draggedItem.name; // Ambil nama GameObject item

        // Dapatkan nilai jual dari EconomyManager
        int sellValue = EconomyManager.Instance.GetSellValue(itemName);
        
        if (sellValue > 0)
        {
            EconomyManager.Instance.AddMoney(sellValue);
            Destroy(draggedItem); // Hancurkan item yang dijual
            Debug.Log($"Item {itemName} dijual! +{sellValue}");
        }
    }
}