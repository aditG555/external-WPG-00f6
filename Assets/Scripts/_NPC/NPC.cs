using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour
{
    public NPCData npcData;
    public string currentProblem;
    public Jamu.jamuType desiredJamu;
    public bool wasServedCorrectly = false; // Menandakan apakah jamu yang diberikan benar

    // Dipanggil oleh NPCManager saat spawn
    public void InitializeNPC() 
    {
        GenerateRandomData();
        
        // Tampilkan dialog masalah selama 5 detik
        DialogManager.Instance.ShowProblemDialog(
            currentProblem, 
            5f
        );
    }

    void GenerateRandomData()
    {
        currentProblem = npcData.possibleProblems[Random.Range(0, npcData.possibleProblems.Length)];
        desiredJamu = npcData.desiredJamuTypes[Random.Range(0, npcData.desiredJamuTypes.Length)];
    }

    public void HandleItemDrop(GameObject jamuItem)
    {
        bool isCorrect = CheckJamu(jamuItem);
        string resultText = isCorrect ? npcData.correctDialog : npcData.wrongDialog;
        Debug.Log("Handle Drop Called");
        
        // Tampilkan dialog hasil
        DialogManager.Instance.ShowResultDialog(
            resultText, 
            2f
        );

        NPCQueue.Instance.UpdateJamuStats(isCorrect);
        
        // Proses transaksi ekonomi
        int baseValue = EconomyManager.Instance.GetJamuBasePrice(jamuItem);
        EconomyManager.Instance.ProcessJamuTransaction(
            npcData.traits,
            isCorrect,
            baseValue
        );
        
        // Hancurkan NPC dan spawn baru
        StartCoroutine(DestroyAndRespawn());
    }

    bool CheckJamu(GameObject jamuItem)
    {
        Jamu jamu = jamuItem.GetComponent<Jamu>();
        return jamu != null && jamu.type == desiredJamu;
    }

    IEnumerator DestroyAndRespawn()
    {
<<<<<<< HEAD
        Destroy(this.gameObject);
        Debug.Log("GameObject Destroyed after being served.");
        NPCManager.Instance.SpawnNewNPC();
        Debug.Log("NPC spawned new NPC after being served.");   
=======
        yield return new WaitForSeconds(1f);
        
        if (gameObject != null)
        {
            Destroy(gameObject);
            
            // Spawn NPC berikutnya jika antrian belum habis
            if (!NPCQueue.Instance.IsQueueEmpty())
            {
                NPCManager.Instance.SpawnNewNPC();
            }
            else
            {
                DayCycleManager.Instance.EndDay();
            }
        }
>>>>>>> parent of d3044b7 (Add Changes)
    }

    public void HandleDayEnd()
    {
        if (!wasServedCorrectly)
        {
            DayCycleManager.Instance.ScheduleRefund(this,EconomyManager.Instance.GetJamuBasePrice(gameObject));
        }
        Destroy(gameObject);
    }
}
