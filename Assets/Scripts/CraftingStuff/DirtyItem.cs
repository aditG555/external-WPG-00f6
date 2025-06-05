using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[CreateAssetMenu(fileName = "New DirtyItmeData", menuName = "Jamu Game/Items")]
public class DirtyItem : ScriptableObject
{
    public Sprite Dirty;
    public GameObject CleanItem;

    public Sprite GetSprite()
    {
        return Dirty;
    }
    public GameObject GetCleanedItem()
    {
        return CleanItem;
    }

}
