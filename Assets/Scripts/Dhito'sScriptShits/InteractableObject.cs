using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public virtual void Interact()
    {
        Debug.Log(gameObject.name + " Interacted");
    }
    void Update()
    {
        
    }
}
