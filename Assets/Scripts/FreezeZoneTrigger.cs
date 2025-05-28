using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

[RequireComponent(typeof(Collider))]
public class FreezeZoneTrigger : MonoBehaviour
{
    [Tooltip("Drag your Player Transform here so we know where to chase.")]
    public Transform player;

    // internal list of everyone we froze
    private readonly HashSet<NavMeshAgent> _frozenAgents = new HashSet<NavMeshAgent>();
    private Collider _col;

    void Awake()
    {
        _col = GetComponent<Collider>();
        _col.isTrigger = true;            // make sure it's a trigger
    }

    void OnTriggerEnter(Collider other)
    {
        var agent = other.GetComponent<NavMeshAgent>();
        var anim  = other.GetComponent<Animator>();
        if (agent != null)
        {
            agent.isStopped = true;       // freeze movement
            if (anim) anim.SetBool("IsWalking", false);
            _frozenAgents.Add(agent);     // remember it
        }
    }

    void OnTriggerExit(Collider other)
    {
        var agent = other.GetComponent<NavMeshAgent>();
        if (agent != null && _frozenAgents.Remove(agent))
        {
            // unfreeze + immediately tell it where to go
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }
    }

    /// <summary>
    /// Call this after you disable the collider so anyone still inside
    /// (who never got an OnTriggerExit) gets un‐stopped and told to chase again.
    /// </summary>
    public void ReleaseAll()
    {
        foreach (var agent in _frozenAgents)
        {
            if (agent == null) continue;
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }
        _frozenAgents.Clear();
    }
}
