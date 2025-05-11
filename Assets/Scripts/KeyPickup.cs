using UnityEngine;

[RequireComponent(typeof(Collider))]
public class KeyPickup : Interactable
{
    public override void OnFocus()
    {
        // (you can flash UI here)
        Debug.Log("Focus on key. Press E to pick up.");
    }

    public override void OnDefocus()
    {
        Debug.Log("Defocus from key.");
    }

    public override void Interact()
    {
        Debug.Log("Picked up key!");
        Inventory.Instance.CollectKey();
        Destroy(gameObject);
    }
}
