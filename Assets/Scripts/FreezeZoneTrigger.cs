using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Collider))]
public class FreezeZoneTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // did an enemy walk in?
        var agent = other.GetComponent<NavMeshAgent>();
        var anim  = other.GetComponent<Animator>();
        if (agent != null)
        {
            agent.isStopped       = true;
            if (anim) anim.SetBool("IsWalking", false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // enemy left, let them go again
        var agent = other.GetComponent<NavMeshAgent>();
        if (agent != null)
            agent.isStopped = false;
    }
}
