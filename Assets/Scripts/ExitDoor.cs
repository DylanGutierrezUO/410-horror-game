using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ExitDoor : MonoBehaviour {
    private enum DoorState { Locked, Unlocked, Open }
    private DoorState state = DoorState.Locked;

    [SerializeField] private Animator doorAnimator; // optional
    [SerializeField] private string unlockTrigger = "OpenDoor"; // Animator trigger name

    void OnEnable() {
        Inventory.OnKeyCollected += Unlock;
    }
    void OnDisable() {
        Inventory.OnKeyCollected -= Unlock;
    }

    void Unlock() {
        if (state != DoorState.Locked) return;
        state = DoorState.Unlocked;
        // Optional: flash UI “Door unlocked”
    }

    void OnTriggerStay(Collider other) {
        if (state != DoorState.Unlocked) return;
        if (!other.CompareTag("Player")) return;
        if (Input.GetKeyDown(KeyCode.E)) OpenDoor();
    }

    void OpenDoor() {
        state = DoorState.Open;
        if (doorAnimator) doorAnimator.SetTrigger(unlockTrigger);
        else transform.Translate(Vector3.up * 3f, Space.World); // fallback slide
        // TODO: notify GameManager to end level
    }
}
