using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Controls the enemy chasing behavior and stopping behavior based on player visibility or spotlight detection.
/// </summary>
public class EnemyChase : MonoBehaviour
{
    public Transform player;         // Reference to the player object (for chasing)
    public Transform playerCamera;   // Reference to the player's camera (for looking detection)

    private NavMeshAgent agent;       // NavMeshAgent component for movement
    private Animator animator;        // Animator component for movement animation

    private bool isSeenByPlayer = false;  // True if player is looking at the enemy
    private bool isSeenByLight = false;   // True if enemy is in an active spotlight

    public float viewAngle = 60f;     // Vision cone width (degrees)
    public float viewDistance = 20f;  // Max distance player can see the enemy

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null || playerCamera == null) return;

        CheckIfSeenByPlayer();

        // Enemy stops if seen by player OR caught in a spotlight
        if (!isSeenByPlayer && !isSeenByLight)
        {
            if (Vector3.Distance(transform.position, player.position) > agent.stoppingDistance)
            {
                agent.SetDestination(player.position);
            }
            else
            {
                agent.ResetPath();
            }
        }
        else
        {
            agent.ResetPath();
        }

        // Update Animator speed based on movement velocity
        if (animator != null)
        {
            animator.SetFloat("Speed", agent.velocity.magnitude);
        }
    }

    /// <summary>
    /// Checks if the player is currently looking at the enemy within a field of view and distance.
    /// </summary>
    void CheckIfSeenByPlayer()
    {
        Vector3 dirToEnemy = (transform.position - playerCamera.position).normalized;
        float angleBetween = Vector3.Angle(playerCamera.forward, dirToEnemy);

        if (angleBetween < viewAngle / 2f && Vector3.Distance(playerCamera.position, transform.position) <= viewDistance)
        {
            isSeenByPlayer = true;
        }
        else
        {
            isSeenByPlayer = false;
        }
    }

    /// <summary>
    /// When enemy enters a spotlight trigger, check if the light is enabled.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Spotlight"))
        {
            Light spotlight = other.GetComponentInParent<Light>();
            if (spotlight != null)
            {
                isSeenByLight = spotlight.enabled;
            }
        }
    }

    /// <summary>
    /// While enemy stays inside the spotlight, keep checking if the light is still on.
    /// </summary>
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Spotlight"))
        {
            Light spotlight = other.GetComponentInParent<Light>();
            if (spotlight != null)
            {
                isSeenByLight = spotlight.enabled;
            }
        }
    }

    /// <summary>
    /// When enemy exits the spotlight trigger, stop considering it seen by light.
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Spotlight"))
        {
            isSeenByLight = false;
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        // Look on the collided object _or any of its parents_ for a PlayerDeathHandler
        var deathHandler = collision.collider.GetComponentInParent<PlayerDeathHandler>();
        if (deathHandler != null)
        {
            deathHandler.Die();
        }
    }


}
