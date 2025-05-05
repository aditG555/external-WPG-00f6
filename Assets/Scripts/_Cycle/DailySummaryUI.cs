using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DailySummaryUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject summaryPanel;
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private TextMeshProUGUI totalServedText;
    [SerializeField] private TextMeshProUGUI correctServedText;
    [SerializeField] private TextMeshProUGUI moneyEarnedText;
    [SerializeField] private Button nextDayButton;

    [Header("Rating Elements")]
    [SerializeField] private GameObject[] starIcons;

    void Start()
    {
        nextDayButton.onClick.AddListener(StartNewDay);
        summaryPanel.SetActive(false);
    }

    public void ShowSummary(int day, int total, int correct, int money, float rating)
    {
        summaryPanel.SetActive(true);
        dayText.text = $"Day {day} Summary";
        totalServedText.text = $"{total}";
        correctServedText.text = $"{correct}";
        moneyEarnedText.text = $"Money Earned: {money}";
        UpdateStarRating(rating);
    }

    void StartNewDay()
    {
        summaryPanel.SetActive(false);
        DayCycleManager.Instance.StartNewDay();
    }

    void UpdateStarRating(float rating)
{
    // Reset semua bintang
    foreach (var star in starIcons)
    {
        star.SetActive(false);
    }
    
    // Aktifkan bintang sesuai rating
    for (int i = 0; i < Mathf.FloorToInt(rating); i++)
    {
        if(i < starIcons.Length) starIcons[i].SetActive(true);
    }
    
    // Handle setengah bintang
    if (rating % 1 >= 0.5f)
    {
        int halfStarIndex = Mathf.FloorToInt(rating);
        if(halfStarIndex < starIcons.Length) 
            starIcons[halfStarIndex].SetActive(true);
    }
}
}