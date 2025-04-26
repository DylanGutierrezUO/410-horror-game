using UnityEngine;
using TMPro;
using System.Collections;

/// <summary>
/// Handles player movement, camera look, cursor locking,
/// and UI interactions (highlight, prompts, fading).
/// Attach this script to the Player GameObject.
/// </summary>
public class PlayerController : MonoBehaviour
{
    // --- Public configuration fields ---
    public float moveSpeed = 5f;           // movement speed (units/sec)
    public float mouseSensitivity = 2f;    // mouse look sensitivity
    public Transform playerCamera;         // reference to the child camera

    public CanvasGroup interactPromptGroup;  // UI canvas group for "Press E" prompt
    public CanvasGroup movementPromptGroup;  // UI canvas group for "WASD to move" prompt

    public float interactDistance = 3f;    // how far we can interact/highlight
    public LayerMask interactableLayer;    // which layer holds interactable objects

    // --- Internal state ---
    private float xRotation = 0f;                   // vertical rotation accumulator
    private bool isCursorLocked = true;             // tracks if cursor is locked
    private HighlightInteractable currentHighlight; // currently highlighted object
    private bool isPromptVisible = false;           // is "Press E" currently showing?
    private bool movementStarted = false;           // has player pressed WASD yet?

    void Start()
    {
        // Lock and hide the cursor at game start
        LockCursor();

        // Initialize UI prompt alphas:
        if (interactPromptGroup != null) interactPromptGroup.alpha = 0f;
        if (movementPromptGroup != null) movementPromptGroup.alpha = 1f;
    }

    void Update()
    {
        MovePlayer();          // handle WASD movement
        LookAround();          // handle mouse look
        CheckCursorUnlock();   // allow ESC to unlock cursor
        CheckMovementStart();  // detect first WASD press to hide movement prompt
        CheckInteraction();    // handle highlighting, prompts, and E-interaction
    }

    /// <summary>
    /// Moves the player based on horizontal (A/D) and vertical (W/S) input.
    /// </summary>
    void MovePlayer()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 moveDir = transform.right * moveX + transform.forward * moveZ;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    /// <summary>
    /// Rotates camera vertically and player body horizontally with mouse movement.
    /// </summary>
    void LookAround()
    {
        if (!isCursorLocked) return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // vertical look (camera)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // horizontal look (player body)
        transform.Rotate(Vector3.up * mouseX);
    }

    /// <summary>
    /// Unlocks the cursor when ESC is pressed.
    /// </summary>
    void CheckCursorUnlock()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            UnlockCursor();
    }

    /// <summary>
    /// Detects the first WASD input and fades out the movement prompt.
    /// </summary>
    void CheckMovementStart()
    {
        if (!movementStarted &&
            (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) ||
             Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)))
        {
            movementStarted = true;
            if (movementPromptGroup != null)
                StartCoroutine(FadeCanvas(movementPromptGroup, 1f, 0f, 1f));
        }
    }

    /// <summary>
    /// Handles highlighting objects, showing/hiding "Press E" prompt, and E-triggered interaction.
    /// </summary>
    void CheckInteraction()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        bool hitInteractable = Physics.Raycast(
            ray,
            out RaycastHit hit,
            interactDistance,
            LayerMask.GetMask("Interactable")
        );

        // Fade the "Press E" prompt in or out
        if (interactPromptGroup != null)
        {
            if (hitInteractable && !isPromptVisible)
            {
                isPromptVisible = true;
                StartCoroutine(FadeCanvas(interactPromptGroup, 0f, 1f, 0.25f));
            }
            else if (!hitInteractable && isPromptVisible)
            {
                isPromptVisible = false;
                StartCoroutine(FadeCanvas(interactPromptGroup, 1f, 0f, 0.25f));
            }
        }

        if (hitInteractable)
        {
            // Highlight the object under the crosshair
            HighlightInteractable hi = hit.collider.GetComponent<HighlightInteractable>();
            if (hi != null)
            {
                if (currentHighlight != null && currentHighlight != hi)
                    currentHighlight.Unhighlight();

                currentHighlight = hi;
                currentHighlight.Highlight();
            }

            // Trigger the object when E is pressed
            if (Input.GetKeyDown(KeyCode.E))
            {
                Interactable interactObj = hit.collider.GetComponent<Interactable>();
                if (interactObj != null)
                    interactObj.Interact();
            }
        }
        else
        {
            // Remove highlight when looking away
            if (currentHighlight != null)
            {
                currentHighlight.Unhighlight();
                currentHighlight = null;
            }
        }
    }

    /// <summary>
    /// Fades a CanvasGroup's alpha from 'from' to 'to' over 'duration' seconds.
    /// </summary>
    IEnumerator FadeCanvas(CanvasGroup cg, float from, float to, float duration)
    {
        float elapsed = 0f;
        cg.alpha = from;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            cg.alpha = Mathf.Lerp(from, to, elapsed / duration);
            yield return null;
        }

        cg.alpha = to;
    }

    /// <summary>
    /// Locks and hides the cursor.
    /// </summary>
    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isCursorLocked = true;
    }

    /// <summary>
    /// Unlocks and shows the cursor.
    /// </summary>
    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isCursorLocked = false;
    }
}
