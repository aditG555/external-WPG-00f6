using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionEsc : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Animator animator;
    public bool TabsOpen = false;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Toogle();
            Debug.Log("Presing Escape");
        }
    }
    public void Toogle()
    {
        if (TabsOpen)
        {
            animator.Play("EscOFF");
            TabsOpen = false;
            GameManager._instance.InputAllowed = true;
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 0f;
            GameManager._instance.InputAllowed = false;
            animator.Play("EscON");
        }
    }
    public void PouseGame()
    {
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }
    public void Exit()
    {
        GameManager._instance.InputAllowed = true;
        SceneManager.LoadScene("MainGame");
    }
}
