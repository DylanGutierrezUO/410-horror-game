using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ExitDoor : MonoBehaviour, IActivatable
{
    enum State { Locked, Unlocked, Open }
    State state = State.Locked;

    [SerializeField] Animator doorAnimator;
    [SerializeField] string openTrigger = "OpenDoor";
    [SerializeField] AudioSource audioSource;         // Reference to the AudioSource
    [SerializeField] AudioClip doorOpenSound;         // Reference to the door open sound

    public string InteractionPrompt
        => state == State.Unlocked
           ? "Press E to Open"
           : "Locked – find the key";

    public void OnActivate()
    {
        if (state != State.Unlocked) return;

        // Play the door open sound
        if (audioSource && doorOpenSound)
        {
            audioSource.PlayOneShot(doorOpenSound);
        }

        state = State.Open;

        if (doorAnimator)
            doorAnimator.SetTrigger(openTrigger);
        else
            transform.Translate(Vector3.up * 3f, Space.World);

        GameManager.Instance.LevelComplete();
    }

    void OnEnable() => Inventory.OnKeyCollected += () => state = State.Unlocked;
    void OnDisable() => Inventory.OnKeyCollected -= () => state = State.Unlocked;
}

