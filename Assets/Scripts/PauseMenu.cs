using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;
    public MonoBehaviour lookScript;

    void Start()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (lookScript != null)
            lookScript.enabled = true;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        Debug.Log("resuming...");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (lookScript != null)
            lookScript.enabled = true;
    }

    void Pause()
    {
        Debug.Log("pausing...");
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (lookScript != null)
            lookScript.enabled = false;
    }

        public void RestartGame()
    {
        Debug.Log("Restarting current level...");
        Time.timeScale = 1f; // Ensure time is normal before loading
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reloads the current scene

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (lookScript != null)
            lookScript.enabled = true;
    }

    public void LoadMainMenu()
    {
        Debug.Log("Loading Main Menu...");
        Time.timeScale = 1f; 
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene("MainMenuScene");
    }
}
