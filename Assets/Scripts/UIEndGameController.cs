using UnityEngine;

public class UIEndGameController : MonoBehaviour
{
    [SerializeField] private GameObject completionPanel;
    [SerializeField] private GameObject deathPanel;

    void Awake()
    {
        // start hidden
        completionPanel.SetActive(false);
        deathPanel.SetActive(false);
    }

    void OnEnable()
    {
        GameManager.OnLevelCompleteEvent += ShowCompletion;
        GameManager.OnPlayerDiedEvent    += ShowDeath;
    }

    void OnDisable()
    {
        GameManager.OnLevelCompleteEvent -= ShowCompletion;
        GameManager.OnPlayerDiedEvent    -= ShowDeath;
    }

    private void ShowCompletion()
    {
        Debug.Log("UIEndGameController: ShowCompletion()");
        completionPanel.SetActive(true);
    }

    private void ShowDeath()
    {
        Debug.Log("UIEndGameController: ShowDeath()");
        deathPanel.SetActive(true);
    }
}
