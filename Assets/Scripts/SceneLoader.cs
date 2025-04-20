using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // List of team member scene names to load additively
    public string[] scenesToLoad = { "AlenScene/Alen", "DylanScene/Dylan", "CalebScene/Caleb", "MichaelScene/Michael" };

    void Start()
    {
        foreach (string sceneName in scenesToLoad)
        {
            SceneManager.LoadSceneAsync("Scenes/" + sceneName, LoadSceneMode.Additive);
        }
    }
}
