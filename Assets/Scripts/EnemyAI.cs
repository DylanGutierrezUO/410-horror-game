using UnityEngine;
using UnityEngine.AI;

// Controls the enemy chasing behavior and animation based on player visibility.
public class EnemyChase : MonoBehaviour
{
    public Transform player;             // Reference to the player the enemy chases.
    private NavMeshAgent agent;           // Reference to the enemy's NavMeshAgent component.
    private Animator animator;            // Reference to the enemy's Animator component.
    private bool isSeen = false;           // Whether the enemy is currently being seen by the player.

    void Start()
    {
        // Get required components at the start.
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // If there is no player assigned, exit early.
        if (player == null) return;

        // Calculate distance between enemy and player.
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (!isSeen) // If the enemy is not being seen.
        {
            if (distanceToPlayer > agent.stoppingDistance)
            {
                // Chase the player if not close enough.
                agent.SetDestination(player.position);
            }
            else
            {
                // Stop moving if already within stopping distance.
                agent.ResetPath();
            }
        }
        else // If the enemy is being seen.
        {
            // Stop moving while being watched.
            agent.ResetPath();
        }

        // Update animation speed parameter based on agent's current velocity.
        if (animator != null)
        {
            animator.SetFloat("Speed", agent.velocity.magnitude);
        }
    }

    // External method to set whether the enemy is being seen.
    public void SetSeen(bool seen)
    {
        isSeen = seen;
    }
}