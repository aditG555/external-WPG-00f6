using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    public List<CraftingSlot> slots;
    public CraftingSlot outputSlot;

    [System.Serializable]
    public class RecipeItem
    {
        public int slotIndex;
        public string itemName;
    }

    [System.Serializable]
    public class CraftingRecipe
    {
        public List<RecipeItem> requiredItems;
        public string resultItem;
    }

    public List<CraftingRecipe> recipes = new List<CraftingRecipe>();

    public void CheckRecipe()
    {
        foreach (var recipe in recipes)
        {
            bool isMatch = true;
            
            foreach (var requiredItem in recipe.requiredItems)
            {
                int index = requiredItem.slotIndex;
                
                if (index >= slots.Count || 
                    slots[index].itemName != requiredItem.itemName)
                {
                    isMatch = false;
                    break;
                }
            }

            if (isMatch)
            {
                outputSlot.SetItem(recipe.resultItem);
                return;
            }
        }
        
        outputSlot.ClearSlot();
    }
}