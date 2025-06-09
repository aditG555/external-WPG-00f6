using UnityEngine;

public class InventoryTab : MonoBehaviour
{
    [SerializeField] AudioSource InventorySound;
    [SerializeField] AudioClip[] AudioClip;
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
        int Rin = Random.Range(3, 20)%2;
        InventorySound.clip = AudioClip[Rin];
        Debug.Log(InventorySound.clip.name + " was Played");
        InventorySound.Play();
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
