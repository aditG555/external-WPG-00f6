using UnityEngine;

public class InventoryTab : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Animator animator;
    public bool TabsOpen = true;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager._instance.InputAllowed)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Toogle();
            }
        }
        
    }
    public void Toogle()
    {
        if (TabsOpen)
        {
            animator.Play("TabDownAnimations");
            //TabsOpen = false;
        }
        else
        {
            animator.Play("TabUpAnimations");
            //TabsOpen=true;
        }
    }
}
