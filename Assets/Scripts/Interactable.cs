using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Interactable : MonoBehaviour
{
    /// <summary>
    /// Called by the player when they press E on this object.
    /// </summary>
    public abstract void Interact();

    /// <summary>
    /// (Optional) called when this object comes into focus (for highlight/UI).
    /// </summary>
    public virtual void OnFocus() { }

    /// <summary>
    /// (Optional) called when this object leaves focus.
    /// </summary>
    public virtual void OnDefocus() { }

    void Reset()
    {
        // ensure we can raycast-hit it
        var col = GetComponent<Collider>();
        col.isTrigger = false;
    }
}
