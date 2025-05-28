using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Interactable : MonoBehaviour, IActivatable
{
  // IActivatable:
  public abstract string InteractionPrompt { get; }
  public void OnActivate() => Interact();

  // your old API:
  public abstract void Interact();
  public virtual void OnFocus()    {}
  public virtual void OnDefocus()  {}

  void Reset()
  {
    var col = GetComponent<Collider>();
    col.isTrigger = false;
  }
}
