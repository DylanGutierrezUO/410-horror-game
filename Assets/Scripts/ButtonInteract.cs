using UnityEngine;
using UnityEngine.AI; // Needed for NavMeshObstacle

public class ButtonInteract : MonoBehaviour, Interactable
{
    public Light spotlight;                 // Light to toggle
    public NavMeshObstacle lightObstacle;    // NavMeshObstacle to activate
    public float cooldownTime = 2f;           // Cooldown between uses
    private bool canActivate = true;          // Whether the button can be pressed

    public void Interact()
    {
        if (!canActivate) return;

        if (spotlight != null)
        {
            // Toggle spotlight
            spotlight.enabled = !spotlight.enabled;
        }

        if (lightObstacle != null)
        {
            // Sync obstacle to match light state
            lightObstacle.enabled = spotlight.enabled;
        }

        canActivate = false;
        Invoke(nameof(ResetCooldown), cooldownTime);
    }

    private void ResetCooldown()
    {
        canActivate = true;
    }
}
