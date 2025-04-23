using UnityEngine;

public class Jamu : MonoBehaviour
{
    public enum jamuType // Tambahkan 'public'
    {
        NULL,
        BerasKencur,
        Temulawak,
        KunyitAsam,
        TehRosella,
        TehAdas,
        TehChamomile
    };
    
    public jamuType type; // Ganti 'jamuType' menjadi 'type'
}