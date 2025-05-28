using UnityEngine;

public class KeyPickup : Interactable
{
  // this is what the player will see
  public override string InteractionPrompt => "Press E to pick up";

  public override void OnFocus()
  {
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
