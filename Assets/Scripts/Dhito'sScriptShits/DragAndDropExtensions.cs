using Unity.VisualScripting;
using UnityEngine;

public static class DragAndDropExtensions
{
    public static void SetParrent(this DragAndDrop dragAndDrop, Transform parent)
    {
        dragAndDrop.ParentAfterDrag = parent;
    }
}