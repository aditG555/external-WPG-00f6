using UnityEngine;
using System.Collections.Generic;
using TMPro; // Pastikan Anda sudah mengimpor TextMeshPro
using UnityEngine.UI; // Untuk menggunakan UI Button

public class DayCycleManager : MonoBehaviour
{
    public static DayCycleManager Instance;

    [Header("Day Settings")]
    [SerializeField] private float dayDuration = 60f; // Durasi hari dalam detik
    [SerializeField] public int currentDay = 1;
    [SerializeField] private TextMeshProUGUI dayText;

    [Header("NPC Refund")]
    public float refundPenaltyMultiplier = 0.5f;

    private float dayTimer;
    private List<NPC> npcsToRefund = new List<NPC>();

    [Header("Queue Settings")]
    [SerializeField] private int npcPerDay = 5;

    [Header("Daily Summary")]
    [SerializeField] private DailySummaryUI dailySummaryUI;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartNewDay();
    }

    void Update()
    {
        dayTimer -= Time.deltaTime;

        if (dayTimer <= 0)
        {
            EndDay();
            StartNewDay();
        }
    }

    public void StartNewDay()
    {
        NPCQueue.Instance.ResetDailyStats();
        NPCQueue.Instance.InitializeQueue(npcPerDay);
        dayTimer = dayDuration;
        currentDay++;
        UpdateDayUI();
        NPCManager.Instance.SpawnNewNPC();
    }

    public void EndDay()
    {
        NPCQueue.Instance.ProcessRefunds();
        NPCManager.Instance.ClearCurrentNPC();
        ShowDailySummary();
    }

    private void ShowDailySummary()
{
    int total, correct;
    NPCQueue.Instance.GetDailyStats(out total, out correct);
    
    // Ambil nilai harian sebelum di-reset
    int dailyMoney = EconomyManager.Instance.GetDailyMoney();
    
    dailySummaryUI.ShowSummary(
        currentDay,
        total,
        correct,
        dailyMoney // Gunakan nilai harian
    );
    
    EconomyManager.Instance.ResetDailyMoney();
}

    public void ScheduleRefund(NPC npc, int baseAmount)
    {
        int penalty = Mathf.RoundToInt(baseAmount * refundPenaltyMultiplier);
        npcsToRefund.Add(npc);
        Debug.Log($"Refund scheduled for {npc.name}. Penalty: {penalty}");
    }

    private void ProcessRefunds()
    {
        foreach (NPC npc in npcsToRefund)
        {
            EconomyManager.Instance.RemoveMoney(
                Mathf.RoundToInt(
                    EconomyManager.Instance.GetJamuBasePrice(npc.gameObject) * refundPenaltyMultiplier
                )
            );
            Debug.Log($"Refund processed for {npc.name}");
        }
        npcsToRefund.Clear();
    }

    private void UpdateDayUI()
    {
        dayText.text = $"Day: {currentDay}";
    }

    // Dipanggil saat player memilih untuk tidur (bisa dihubungkan ke tombol UI)
    public void ForceEndDay()
    {
        EndDay();
        StartNewDay();
    }
}
