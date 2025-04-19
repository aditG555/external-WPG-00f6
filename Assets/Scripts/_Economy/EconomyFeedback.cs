using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class EconomyFeedback : MonoBehaviour
{
    public TextMeshProUGUI popupText;
    public Color gainColor;
    public Color lossColor;

    public void ShowTransactionEffect(int amount, bool isGain)
    {
        popupText.text = (isGain ? "+" : "-") + amount.ToString();
        popupText.color = isGain ? gainColor : lossColor;
        StartCoroutine(AnimateEffect());
    }

    IEnumerator AnimateEffect()
    {
        // Animasi teks melayang
        float duration = 1f;
        Vector3 startPos = transform.position;
        
        for(float t=0; t<duration; t+=Time.deltaTime)
        {
            transform.position = startPos + Vector3.up * t*50;
            popupText.alpha = 1 - t/duration;
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
