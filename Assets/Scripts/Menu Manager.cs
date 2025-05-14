using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject controlsPanel;

    public void StartGame()
    {
        // Replace "GameScene" with the name of your game scene
        SceneManager.LoadScene("Main");
    }

    // Called by the "Exit Game" button
    public void ExitGame()
    {
        // Note: Application.Quit() only works in a built application.
        Application.Quit();
    }

    // Called by the "Controls" button to show the submenu
    public void OpenControlsMenu()
    {
        mainMenuPanel.SetActive(false);
        controlsPanel.SetActive(true);
    }

    // Called by the "Back" button in the Controls submenu
    public void BackToMainMenu()
    {
        controlsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}
