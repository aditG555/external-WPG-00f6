using UnityEngine;

public class ESCOption : MonoBehaviour
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
        }
    }
    public void Toogle()
    {
        if (TabsOpen)
        {
            animator.Play("EscOFF");
            //TabsOpen = false;
        }
        else
        {
            animator.Play("EscON");
            //TabsOpen=true;
        }
    }
}
