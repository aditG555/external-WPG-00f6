using UnityEngine;
using System.Collections.Generic;
using TMPro; // Pastikan Anda sudah mengimpor TextMeshPro
using UnityEngine.UI; // Untuk menggunakan UI Button

public class DayCycleManager : MonoBehaviour
{
    public static DayCycleManager Instance;

    [Header("Day Settings")]
<<<<<<< Updated upstream
    [SerializeField] private float dayDuration = 60f; // Durasi hari dalam detik
    [SerializeField] private int currentDay = 1;
=======
    public float dayDuration = 60f; // Durasi hari dalam detik
    [SerializeField] public int currentDay = 1;
>>>>>>> Stashed changes
    [SerializeField] private TextMeshProUGUI dayText;

    [Header("NPC Refund")]
    [SerializeField] private float refundPenaltyMultiplier = 0.5f;

    [Header("LogBook")]
    [SerializeField] GameObject LogBook;

    public float dayTimer;
    private List<NPC> npcsToRefund = new List<NPC>();

    bool EYES = true;

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
            if (EYES)
            {
                EndDay();
                EYES = false;
            }
            
            //StartNewDay();
        }
    }

    public void StartNewDay()
    {
        dayTimer = dayDuration;
        currentDay++;
        UpdateDayUI();
        
        // Spawn NPC pertama hari ini
        NPCManager.Instance.SpawnNewNPC();
    }

    public void EndDay()
    {
        // Proses refund untuk NPC yang salah diberi jamu
        ProcessRefunds();

        LogBook.SetActive(true);

        
        // Bersihkan NPC yang tersisa
        NPCManager.Instance.ClearCurrentNPC();
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