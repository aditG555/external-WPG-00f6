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

    void Start()
    {
        nextDayButton.onClick.AddListener(StartNewDay);
        summaryPanel.SetActive(false);
    }

    public void ShowSummary(int day, int total, int correct, int money)
    {
        summaryPanel.SetActive(true);
        dayText.text = $"Day {day} Summary";
        totalServedText.text = $"{total}";
        correctServedText.text = $"{correct}";
        moneyEarnedText.text = $"Money Earned: {money}";
    }

    void StartNewDay()
    {
        summaryPanel.SetActive(false);
        DayCycleManager.Instance.StartNewDay();
    }
}