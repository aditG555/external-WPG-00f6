using UnityEngine;

public class MouseInteractable : InteractableObject
{
    private void OnMouseDown()
    {
        Interact();
    }
    public override void Interact()
    {
        Destroy(gameObject);
    }
}
