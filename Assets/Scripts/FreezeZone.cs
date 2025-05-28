using UnityEngine;

public class FreezeZone : MonoBehaviour
{
    // As soon as the enemy steps into this volume, shut off its AI component:
    private void OnTriggerEnter(Collider other)
    {
        var ai = other.GetComponent<EnemyFollowWhenNotSeen>();
        if (ai != null) ai.enabled = false;
    }

    // When it leaves, turn the AI back on:
    private void OnTriggerExit(Collider other)
    {
        var ai = other.GetComponent<EnemyFollowWhenNotSeen>();
        if (ai != null) ai.enabled = true;
    }
}
