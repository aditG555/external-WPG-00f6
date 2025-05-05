using UnityEngine;
using System.Collections.Generic;

public class NPCQueue : MonoBehaviour
{
    public static NPCQueue Instance;

    private Queue<NPCData> npcDataQueue = new Queue<NPCData>();
    private List<NPC> spawnedNPCs = new List<NPC>();
    private int totalJamuServed;
    private int correctJamuServed;

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

    // Inisialisasi antrian dengan jumlah NPC tertentu
    public void InitializeQueue(int numberOfNPCs)
    {
        npcDataQueue.Clear();
        spawnedNPCs.Clear();
        totalJamuServed = 0;
        correctJamuServed = 0;

        for (int i = 0; i < numberOfNPCs; i++)
        {
            NPCData randomData = NPCManager.Instance.possibleNPCData[
                Random.Range(0, NPCManager.Instance.possibleNPCData.Length)
            ];
            npcDataQueue.Enqueue(randomData);
        }
    }

    // Mengambil data NPC berikutnya dari antrian
    public NPCData GetNextNPCData()
    {
        if (npcDataQueue.Count == 0) return null;
        return npcDataQueue.Dequeue();
    }

    // Mendaftarkan NPC yang sudah di-spawn
    public void RegisterSpawnedNPC(NPC npc)
    {
        spawnedNPCs.Add(npc);
    }

    // Memproses refund untuk NPC yang tidak dilayani dengan benar
    public void ProcessRefunds()
    {
        foreach (NPC npc in spawnedNPCs)
        {
            if (!npc.wasServedCorrectly && npc != null)
            {
                int penalty = Mathf.RoundToInt(
                    EconomyManager.Instance.GetJamuBasePrice(npc.gameObject) * 
                    DayCycleManager.Instance.refundPenaltyMultiplier
                );
                EconomyManager.Instance.RemoveMoney(penalty);
            }
        }
    }

    // Update statistik jamu
    public void UpdateJamuStats(bool isCorrect)
    {
        totalJamuServed++;
        if (isCorrect) correctJamuServed++;
    }

    // Mendapatkan statistik hari ini
    public void GetDailyStats(out int total, out int correct)
    {
        total = totalJamuServed;
        correct = correctJamuServed;
        
    }

    // Mengecek apakah antrian sudah kosong
    public bool IsQueueEmpty()
    {
        return npcDataQueue.Count == 0;
    }

    public void ResetDailyStats()
{
    totalJamuServed = 0;
    correctJamuServed = 0;
    spawnedNPCs.Clear();
}
}
