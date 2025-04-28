using UnityEngine;
using UnityEngine.AI;

public class EnemyChase : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    private Animator animator;
    private bool isSeen = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();            
    }

    void Update()
    {
        if (player == null) return;

        if (!isSeen)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            agent.ResetPath();
        }

        if (animator != null)
        {
            animator.SetFloat("Speed", agent.velocity.magnitude);
        }
    }

    public void SetSeen(bool seen)
    {
        isSeen = seen;
    }

   private void OnTriggerEnter(Collider other)
    {
        Light spotlight = other.GetComponent<Light>();
        if (other.CompareTag("Spotlight"))
        {
            if (spotlight != null)
            {
                SetSeen(spotlight.enabled);
            }
        }
    }
    void OnTriggerStay(Collider other)
    {
        Light spotlight = other.GetComponent<Light>();
        if (other.CompareTag("Spotlight"))
        {
            if (spotlight != null)
            {
                SetSeen(spotlight.enabled);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Spotlight"))
        {
            SetSeen(false);
        }
    }
}