using UnityEngine;
using TMPro;

public class NoteInteractor : MonoBehaviour
{
    public float interactDistance = 3f;
    public Camera playerCamera;
    public MonoBehaviour lookScript;

    public GameObject interactPrompt;
    public GameObject noteUIPanel;
    public TextMeshProUGUI noteText;

    private Note currentNote = null;
    private bool noteOpen = false;

    void Update()
    {
        if (!noteOpen)
        {
            CheckForNote();
            if (currentNote != null && Input.GetKeyDown(KeyCode.E))
            {
                OpenNote(currentNote.message);
            }
        }

        if (noteOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseNote();
        }
    }

    void CheckForNote()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            if (hit.collider.CompareTag("Note"))
            {
                Note note = hit.collider.GetComponent<Note>();
                if (note != null)
                {
                    currentNote = note;
                    interactPrompt.SetActive(true);
                    return;
                }
            }
        }

        currentNote = null;
        interactPrompt.SetActive(false);
    }

    void OpenNote(string message)
    {
        noteUIPanel.SetActive(true);
        noteText.text = message;
        interactPrompt.SetActive(false);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        noteOpen = true;

        if (lookScript != null)
            lookScript.enabled = false;

    }

    void CloseNote()
    {
        noteUIPanel.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        noteOpen = false;

        if (lookScript != null)
            lookScript.enabled = true;
    }
}