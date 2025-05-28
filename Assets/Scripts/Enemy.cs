using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

// Controls enemy AI behavior: follows the player when not being watched,
// stops when looked at, and triggers a game over attack if close enough.
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class EnemyFollowWhenNotSeen : MonoBehaviour
{
    public Transform player;              // Player's transform
    public Camera   playerCamera;         // Reference to the player's camera
    public MonoBehaviour playerLookScript;// (unused here, for disabling on death)
    public float    detectionDistance = 20f;
    public float    fieldOfView       = 60f;

    private Animator      animator;
    private NavMeshAgent  agent;

    // --- NEW: freeze‐by‐light flag ---
    private bool isFrozenByLight = false;

    void Start()
    {
        // Stops enemy at the player's feet
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 0f;

        animator = GetComponent<Animator>();
        agent    = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // --- NEW: if frozen by light, do nothing in Update() ---
        if (isFrozenByLight)
            return;

        if (player == null) return;

        // determine if player is looking
        bool isPlayerLooking = false;
        Vector3 camOrig = playerCamera.transform.position;
        Vector3 dirToEn = (transform.position - camOrig).normalized;
        float   ang    = Vector3.Angle(playerCamera.transform.forward, dirToEn);
        if (ang < fieldOfView/2f)
        {
            if (Physics.Raycast(camOrig, dirToEn, out var hit, detectionDistance)
             && hit.transform == transform)
            {
                isPlayerLooking = true;
            }
        }

        float distance = Vector3.Distance(transform.position, player.position);

        Debug.DrawRay(player.position, player.forward * 2f, Color.green);
        Debug.DrawRay(player.position, (transform.position-player.position).normalized * 2f, Color.red);

        // movement logic
        if (!isPlayerLooking && distance < detectionDistance && distance > agent.stoppingDistance)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
            animator.SetBool("IsWalking", true);
        }
        else
        {
            agent.isStopped = true;
            animator.SetBool("IsWalking", false);
        }

        // face player
        Vector3 lookPos = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(lookPos);
    }

    // --- NEW: catch freeze‐zone triggers ---
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FreezeZone"))
        {
            isFrozenByLight     = true;
            agent.isStopped     = true;
            animator.SetBool("IsWalking", false);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("FreezeZone"))
        {
            isFrozenByLight = false;
        }
    }

    void OnEnable()
    {
        GameManager.OnLevelCompleteEvent += HandleLevelComplete;
    }

    void OnDisable()
    {
        GameManager.OnLevelCompleteEvent -= HandleLevelComplete;
    }

    private void HandleLevelComplete()
    {
        Destroy(gameObject);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Player"))
            GameManager.Instance.PlayerDied();
    }
}
