using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

[RequireComponent(typeof(Collider))]
public class FreezeZoneTrigger : MonoBehaviour
{
    [Tooltip("Only freeze while this lamp is on")]
    [SerializeField] private Light lamp;

    [Tooltip("Speed to apply when 'frozen'")]
    [SerializeField] private float freezeSpeed = 1f;

    // store each agent's original speed so we can restore it
    private Dictionary<NavMeshAgent, float> _originalSpeeds = new Dictionary<NavMeshAgent, float>();

    void Awake()
    {
        var col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    void OnTriggerEnter(Collider other) => TryFreeze(other);
    void OnTriggerStay(Collider other)  => TryFreeze(other);

    void OnTriggerExit(Collider other)
    {
        var agent = other.GetComponent<NavMeshAgent>();
        if (agent != null && _originalSpeeds.ContainsKey(agent))
        {
            // restore full speed
            agent.speed = _originalSpeeds[agent];
            _originalSpeeds.Remove(agent);

            // (optional) immediately reassign destination so they start moving again
            agent.SetDestination(agent.destination);  
        }
    }

    private void TryFreeze(Collider other)
    {
        if (lamp == null || !lamp.enabled) return;

        var agent = other.GetComponent<NavMeshAgent>();
        if (agent != null && !_originalSpeeds.ContainsKey(agent))
        {
            // stash original
            _originalSpeeds[agent] = agent.speed;
            // apply tiny crawl speed instead of 0
            agent.speed = freezeSpeed;
        }
    }

    // Called manually from your Flicker controller if you want a global un-freeze
    public void ReleaseAll()
    {
        foreach (var kv in _originalSpeeds)
            if (kv.Key != null)
                kv.Key.speed = kv.Value;
        _originalSpeeds.Clear();
    }

    // If the zone gets disabled, make sure nobody stays frozen
    void OnDisable() => ReleaseAll();
}
