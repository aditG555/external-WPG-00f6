using UnityEngine;
using TMPro;
using System.Collections;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance;

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private GameObject dialogPanel;

    private Coroutine currentDialogRoutine; // Tambahkan variabel ini

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

    public void ShowProblemDialog(string problemText, float duration)
    {
        // Hentikan dialog sebelumnya jika ada
        if (currentDialogRoutine != null)
        {
            StopCoroutine(currentDialogRoutine);
        }
        currentDialogRoutine = StartCoroutine(DialogRoutine(problemText, duration));
    }

    public void ShowResultDialog(string resultText, float duration)
    {
        // Hentikan dialog sebelumnya jika ada
        if (currentDialogRoutine != null)
        {
            StopCoroutine(currentDialogRoutine);
        }
        currentDialogRoutine = StartCoroutine(DialogRoutine(resultText, duration));
    }

    IEnumerator DialogRoutine(string text, float duration)
    {
        dialogPanel.SetActive(true);
        dialogText.text = text;
        
        yield return new WaitForSeconds(duration);
        
        dialogPanel.SetActive(false);
        currentDialogRoutine = null; // Reset coroutine setelah selesai

        
    }
}