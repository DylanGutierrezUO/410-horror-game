using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // existing events
    public static event Action OnLevelCompleteEvent;
    public static event Action OnPlayerDiedEvent;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        // Restart the current scene when the player presses R
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Restarting level...");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void LevelComplete()
    {
        Debug.Log("GameManager: LevelComplete()");
        OnLevelCompleteEvent?.Invoke();
    }

    public void PlayerDied()
    {
        Debug.Log("GameManager: PlayerDied()");
        OnPlayerDiedEvent?.Invoke();
    }
}
