using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour
{
    public NPCData npcData;
    public string currentProblem;
    public string desiredJamu;
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
        
        // Tampilkan dialog hasil
        DialogManager.Instance.ShowResultDialog(
            resultText, 
            2f
        );
        
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
        return jamu != null && jamu.jamuType == desiredJamu;
    }

    IEnumerator DestroyAndRespawn()
{
    yield return new WaitForSeconds(1f);
    
    // Pastikan NPC belum dihancurkan oleh proses lain (misalnya hari berakhir)
    if (gameObject != null)
    {
        Destroy(gameObject);
        Debug.Log("GameObject Destroyed after being served.");
        NPCManager.Instance.SpawnNewNPC();
        Debug.Log("NPC spawned new NPC after being served.");   
    }
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
