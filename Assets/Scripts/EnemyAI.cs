using UnityEngine;
using UnityEngine.AI;

public class EnemyChase : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    private bool isSeen = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
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
    }

    public void SetSeen(bool seen)
    {
        isSeen = seen;
    }
}