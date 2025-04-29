using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public NPCData npcData;
    public string currentProblem;
    public Jamu.jamuType desiredJamu;
    public Sprite Body;
    public Sprite Head;
    public NPCLooksData SpritesData;
    public bool wasServedCorrectly = false; // Menandakan apakah jamu yang diberikan benar

    Animator animator;

    // Dipanggil oleh NPCManager saat spawn
    public void InitializeNPC() 
    {
        GenerateRandomData();
        Body = SpritesData.Body;
        Head = SpritesData.Head;
        
        // Tampilkan dialog masalah selama 5 detik
        DialogManager.Instance.ShowProblemDialog(
            currentProblem, 
            5f
        );
    }
    public void InitializeNPC(NPCData Data,NPCLooksData Looks)
    {
        GenerateRandomData();
        Body = Looks.Body;
        Head = Looks.Head;

        // Tampilkan dialog masalah selama 5 detik
        DialogManager.Instance.ShowProblemDialog(
            currentProblem,
            5f
        );
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        GetComponent<Image>().sprite = Body;
        gameObject.transform.Find("Head").GetComponent<Image>().sprite = Head;
        gameObject.transform.Find("Body").GetComponent<Image>().sprite = Body;
        GetComponentInChildren<Image>().sprite = Head;
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
        animator.Play("NPCExitAnim");
    yield return new WaitForSeconds(1f);
    
    // Pastikan NPC belum dihancurkan oleh proses lain (misalnya hari berakhir)
    if (gameObject != null)
    {
        Destroy(this.gameObject);
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
        StartCoroutine(DestroyAndRespawn());
        //Destroy(gameObject);
    }
}
