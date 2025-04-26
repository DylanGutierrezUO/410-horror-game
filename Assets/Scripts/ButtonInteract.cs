using UnityEngine;

public class ButtonInteract : MonoBehaviour, Interactable
{
    public ParticleSystem effect;
    public float cooldownTime = 2f;
    private bool canActivate = true;

    public void Interact()
    {
        if (!canActivate) return;

        effect.Play();
        canActivate = false;
        Invoke(nameof(ResetCooldown), cooldownTime);
    }

    private void ResetCooldown()
    {
        canActivate = true;
    }
}
