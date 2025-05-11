using UnityEngine;

public class ExitDoorInteractable : Interactable
{
    [Tooltip("Only opens if Inventory.HasKey==true")]
    public UnityEngine.UI.Text promptText; // optional: "Press E to exit"

    private bool unlocked;

    void OnEnable()
    {
        // listen when key is picked up
        Inventory.OnKeyCollected += () => unlocked = true;
    }

    void OnDisable()
    {
        Inventory.OnKeyCollected -= () => unlocked = true;
    }

    public override void Interact()
    {
        if (!unlocked)
        {
            // you could flash "You need a key!" here
            if (promptText != null)
                promptText.text = "You need a key.";
            return;
        }

        // fire level complete
        Debug.Log("Exit interact!");
        GameManager.Instance.LevelComplete();
    }

    public override void OnFocus()
    {
        if (promptText != null)
            promptText.text = unlocked
                ? "Press E to Exit"
                : "Locked. Find the key.";
    }

    public override void OnDefocus()
    {
        if (promptText != null)
            promptText.text = "";
    }
}
