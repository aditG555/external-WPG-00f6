using UnityEngine;
using UnityEngine.UI;

public class RecepyBookCTR : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Image RecepyPage;
    public void SetSprite(Sprite sprt)
    {
        RecepyPage.sprite = sprt;
    }
}
