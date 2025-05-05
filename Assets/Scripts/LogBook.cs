using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogBook : MonoBehaviour
{
    [SerializeField] UnityEvent Close;
    [SerializeField] TextMeshProUGUI _txtJamuYgDiterima;
    [SerializeField] TextMeshProUGUI _txtJamuYgTidakDiterima;
    [SerializeField] TextMeshProUGUI _txtTotalPenghasilan;
    [SerializeField] TextMeshProUGUI _Popularity;
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void OnEnable()
    {
        _Popularity.text += " "+EconomyManager.Instance.Popularity.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EndDay()
    {
        SceneManager.LoadScene("MainGameTrue");
    }
    public void ClosingBook()
    {
        animator.Play("LogBookClose");
    }
    //IEnumerator ClosingBook()
    //{
    //    animator.Play("");
    //    yield return new WaitForSeconds(1f);
    //}
}
