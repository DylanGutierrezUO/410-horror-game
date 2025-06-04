using UnityEngine;

[RequireComponent(typeof(EnemyFollowWhenNotSeen))]
public class EnemyToggleSelf : MonoBehaviour
{
    // Which key should toggle this one Enemy on/off?
    [SerializeField] private KeyCode toggleKey = KeyCode.K;

    // Cached reference to our EnemyFollowWhenNotSeen component:
    private EnemyFollowWhenNotSeen _enemyAI;

    private void Awake()
    {
        // Grab the EnemyFollowWhenNotSeen on this same GameObject
        _enemyAI = GetComponent<EnemyFollowWhenNotSeen>();
        if (_enemyAI == null)
            Debug.LogWarning("EnemyToggleSelf could not find EnemyFollowWhenNotSeen on this GameObject.");
    }

    private void Update()
    {
        // When the player presses “K”, flip the enabled state of our own EnemyFollowWhenNotSeen:
        if (Input.GetKeyDown(toggleKey))
        {
            if (_enemyAI != null)
            {
                bool isCurrentlyOn = _enemyAI.enabled;
                _enemyAI.enabled = !isCurrentlyOn;
            }
        }
    }
}
