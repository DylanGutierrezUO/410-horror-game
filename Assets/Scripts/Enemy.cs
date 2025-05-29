using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

// Controls enemy AI behavior: follows the player when not being watched,
// stops when looked at, and triggers a game over attack if close enough.
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class EnemyFollowWhenNotSeen : MonoBehaviour
{
    [Header("Player Tracking")]
    public Transform player;        // reference to the player's Transform
    public Camera   playerCamera;   // reference to the player's camera

    [Header("Detection Settings")]
    public float detectionDistance = 20f;
    public float fieldOfView       = 60f;

    // Components
    private NavMeshAgent agent;
    private Animator     animator;

    void Start()
    {
        agent    = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null) return;

        // 1) Check if the player is looking at us
        Vector3 camPos    = playerCamera.transform.position;
        Vector3 toEnemy   = (transform.position - camPos).normalized;
        bool   isLooking  = false;
        float  angle      = Vector3.Angle(playerCamera.transform.forward, toEnemy);

        if (angle < fieldOfView * 0.5f &&
            Physics.Raycast(camPos, toEnemy, out RaycastHit hit, detectionDistance) &&
            hit.transform == transform)
        {
            isLooking = true;
        }

        // 2) Check distance
        float dist = Vector3.Distance(transform.position, player.position);

        // 3) Decide to move or idle
        if (!isLooking && dist < detectionDistance)
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

        // 4) Always face the player on the Y axis
        Vector3 lookAtTarget = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(lookAtTarget);
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
