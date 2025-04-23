// NPCData.cs
using UnityEngine;

[CreateAssetMenu(fileName = "New NPC Data", menuName = "Jamu Game/NPC Data")]
public class NPCData : ScriptableObject
{
    public string[] possibleProblems; // Masalah yang bisa dialami NPC
    public string[] desiredJamuTypes; // Jenis jamu yang diinginkan
    public NPCTrait[] traits; // Sifat-sifat NPC
    public string correctDialog;
    public string wrongDialog;
    public string refundDialog;
}

