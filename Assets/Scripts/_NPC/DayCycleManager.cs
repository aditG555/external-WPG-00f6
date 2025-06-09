using UnityEngine;
using System.Collections.Generic;
using TMPro; // Pastikan Anda sudah mengimpor TextMeshPro
using System.Collections;
using UnityEngine.Events; // Untuk menggunakan UI Button

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
    
    [Header("Rating System")]
    [SerializeField] private RatingSystem ratingSystem;

    [Header("Audio Event")]
    [SerializeField] UnityEvent OnEndDay;


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
        // StartCoroutine(ProcessRefundNPC());

        NPCQueue.Instance.ResetDailyStats();
        NPCQueue.Instance.InitializeQueue(npcPerDay);
        dayTimer = dayDuration;
        currentDay++;
        UpdateDayUI();
        NPCManager.Instance.SpawnNewNPC();
        Debug.Log($"Starting Day {currentDay} with {npcPerDay} NPCs.");
        
    }

   

    public void EndDay()
    {
        OnEndDay.Invoke();
        NPCQueue.Instance.ProcessRefunds();
        NPCManager.Instance.ClearCurrentNPC();
        ShowDailySummary();
    }

    private void ShowDailySummary()
    {
        int total, correct;
        NPCQueue.Instance.GetDailyStats(out total, out correct);
        
        // Tambahkan hasil hari ini ke total kumulatif
        RatingSystem.Instance.AddDayResults(correct, total);
        
        // Dapatkan rating kumulatif
        float rating = RatingSystem.Instance.GetCurrentRating();
        
        // Hitung NPC untuk hari berikutnya berdasarkan rating kumulatif
        int nextDayNPC = RatingSystem.Instance.CalculateNPCForNextDay(currentDay);
        
        // Update NPC per hari berikutnya
        npcPerDay = nextDayNPC;
        
        dailySummaryUI.ShowSummary(
            currentDay,
            total,
            correct,
            EconomyManager.Instance.GetDailyMoney(),
            rating
        );
        
        EconomyManager.Instance.ResetDailyMoney();
    }

    public void ScheduleRefund(NPC npc, int baseAmount)
    {
        int penalty = Mathf.RoundToInt(baseAmount * refundPenaltyMultiplier);
        npcsToRefund.Add(npc);
        Debug.Log($"Refund scheduled for {npc.name}. Penalty: {penalty}");
    }

    private IEnumerator ProcessRefundNPC()
{
    List<NPCQueue.RefundEntry> refunds = NPCQueue.Instance.DequeueAllRefunds();
    Debug.Log($"Jumlah refund yang akan diproses: {refunds.Count}");

    foreach (var refund in refunds)
        {
            // Access public fields from NPCManager
            GameObject npcPrefab = NPCManager.Instance.npcPrefabs[
                Random.Range(0, NPCManager.Instance.npcPrefabs.Length)
            ];

            Transform spawnPoint = NPCManager.Instance.spawnPoints[
                NPCManager.Instance.currentSpawnIndex
            ];

            NPCManager.Instance.currentSpawnIndex =
                (NPCManager.Instance.currentSpawnIndex + 1) % NPCManager.Instance.spawnPoints.Length;

            GameObject refundNPC = Instantiate(
                npcPrefab,
                spawnPoint.position,
                Quaternion.identity,
                NPCManager.Instance.canvasTransform
            );

            NPC npcComponent = refundNPC.GetComponent<NPC>();
            npcComponent.isRefundNPC = true;
            npcComponent.npcData = refund.npcData;
            npcComponent.SpritesData = NPCManager.Instance.possibleNPCLooks[
                Random.Range(0, NPCManager.Instance.possibleNPCLooks.Length)
            ];
            npcComponent.InitializeNPC();

            DialogManager.Instance.ShowProblemDialog(
                npcComponent.currentProblem,
                5f
            );
            EconomyManager.Instance.RemoveMoney(refund.baseAmount);
            Debug.Log($"Refund NPC {npcComponent.name} spawned with penalty: {refund.baseAmount}");

            // npcComponent.StartCoroutine(npcComponent.DestroyAndRespawn());
            npcsToRefund.Remove(npcComponent);
            NPCQueue.Instance.RegisterSpawnedNPC(npcComponent);

            yield return new WaitWhile(() => refundNPC != null);
        }
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
