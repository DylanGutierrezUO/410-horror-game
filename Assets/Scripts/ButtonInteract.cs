using UnityEngine;

public class ButtonInteract : MonoBehaviour, Interactable
{
    public Light spotlight;
    public float cooldownTime = 2f;
    private bool canActivate = true;

    public void Interact()
    {
        if (!canActivate) return;

        if (spotlight != null)
        {
            spotlight.enabled = !spotlight.enabled;
        }
        canActivate = false;
        Invoke(nameof(ResetCooldown), cooldownTime);
    }

    private void ResetCooldown()
    {
        canActivate = true;
    }
}
