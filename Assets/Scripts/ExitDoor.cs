using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ExitDoor : MonoBehaviour, IActivatable
{
    enum State { Locked, Unlocked, Open }
    State state = State.Locked;

    [SerializeField] Animator doorAnimator;
    [SerializeField] string openTrigger = "OpenDoor";

    public string InteractionPrompt
        => state == State.Unlocked
           ? "Press E to Open"
           : "Locked – find the key";

    public void OnActivate()
    {
        if (state != State.Unlocked) return;
        state = State.Open;
        if (doorAnimator) doorAnimator.SetTrigger(openTrigger);
        else transform.Translate(Vector3.up * 3f, Space.World);
        GameManager.Instance.LevelComplete();
    }

    void OnEnable()  => Inventory.OnKeyCollected += () => state = State.Unlocked;
    void OnDisable() => Inventory.OnKeyCollected -= () => state = State.Unlocked;
}
