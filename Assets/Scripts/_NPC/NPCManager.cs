using UnityEngine;
using System.Collections.Generic;

public class NPCManager : MonoBehaviour
{
    public static NPCManager Instance;

    [Header("NPC Settings")]
    [SerializeField] private GameObject[] npcPrefabs;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private NPCData[] possibleNPCData;
    [SerializeField] private NPCLooksData[] possibleNPCLooks;

    [SerializeField] private Transform canvasTransform; 
    
    private GameObject currentNPC;
    private int currentSpawnIndex;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        SpawnNewNPC();
    }

    public void SpawnNewNPC()
    {
        // Hancurkan NPC lama jika ada
        if (currentNPC != null) Destroy(currentNPC);

        NPCData data = NPCQueue.Instance.GetNextNPCData();
        if (data == null) return;
        
        // Pilih spawn point secara bergantian
        Transform spawnPoint = spawnPoints[currentSpawnIndex];
        currentSpawnIndex = (currentSpawnIndex + 1) % spawnPoints.Length;

        // Instantiate NPC baru
        GameObject npcPrefab = npcPrefabs[Random.Range(0, npcPrefabs.Length)];
        currentNPC = Instantiate(npcPrefab, spawnPoint.position, Quaternion.identity, canvasTransform);

        
        // Setup data NPC
        NPC npcComponent = currentNPC.GetComponent<NPC>();
        npcComponent.npcData = data;
        npcComponent.SpritesData = possibleNPCLooks[Random.Range(0, possibleNPCLooks.Length)];
        NPCQueue.Instance.RegisterSpawnedNPC(npcComponent);
        npcComponent.InitializeNPC();
        //npcComponent.InitializeNPC(); // Memicu dialog otomatis
        
        //npcComponent.npcData = possibleNPCData[Random.Range(0, possibleNPCData.Length)];
        //npcComponent.InitializeNPC(possibleNPCData[Random.Range(0, possibleNPCData.Length)], possibleNPCLooks[Random.Range(0, possibleNPCLooks.Length)]);


    }

    public void ClearCurrentNPC()
{
    if (currentNPC != null)
    {
        currentNPC.GetComponent<NPC>().HandleDayEnd();
    }
}
}