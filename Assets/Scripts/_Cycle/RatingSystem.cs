using UnityEngine;

public class RatingSystem : MonoBehaviour
{
    public static RatingSystem Instance;

    [Header("Rating Settings")]
    [SerializeField] private int maxNPCPerDay = 15;
    [SerializeField] private int baseNPCIncrease = 1;
    
    private int totalCorrect = 0;
    private int totalNPC = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Tambahkan hasil hari ini ke total kumulatif
    public void AddDayResults(int correct, int total)
    {
        totalCorrect += correct;
        totalNPC += total;
    }

    // Hitung rating 0-5 berdasarkan akurasi kumulatif
    public float GetCurrentRating()
    {
        if (totalNPC == 0) return 0;
        float percentage = (float)totalCorrect / totalNPC;
        return Mathf.Round(percentage * 50f) / 10f;
    }

    // Tentukan jumlah NPC untuk hari berikutnya berdasarkan rating kumulatif
    public int CalculateNPCForNextDay(int currentDay)
    {
        float rating = GetCurrentRating();
        
        // Base NPC bertambah seiring hari
        int baseNPC = Mathf.FloorToInt(currentDay * 0.5f) + baseNPCIncrease;
        
        // Adjust dengan rating (rating 5 = +50% NPC)
        int adjustedNPC = Mathf.RoundToInt(baseNPC * (1 + rating/10f));
        
        // Clamping
        return Mathf.Clamp(adjustedNPC, baseNPCIncrease, maxNPCPerDay);
    }

    // Reset total (opsional untuk fitur restart game)
    public void ResetRating()
    {
        totalCorrect = 0;
        totalNPC = 0;
    }
}