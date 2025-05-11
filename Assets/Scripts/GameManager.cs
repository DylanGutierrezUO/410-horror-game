using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // existing…
    public static event Action OnLevelCompleteEvent;
    public static event Action OnPlayerDiedEvent;     // ← NEW

    void Awake()
    {
        if (Instance==null) Instance=this;
        else Destroy(gameObject);
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
