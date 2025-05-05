using System.Collections;
using UnityEngine;
using TMPro;

public class PlayerDeathHandler : MonoBehaviour
{
    [Tooltip("CanvasGroup on the DeathScreenCanvas")]
    public CanvasGroup deathScreenGroup;

    [Tooltip("The YOU DIED Text GameObject (child of the canvas)")]
    public GameObject deathText;

    [Tooltip("How many seconds to fade to black")]
    public float fadeDuration = 2f;

    void Start()
    {
        // Make sure at game start the screen is clear and text is hidden
        deathScreenGroup.alpha = 0f;
        deathText.SetActive(false);
    }

    /// <summary>
    /// Call this when the player dies (e.g. enemy collision)
    /// </summary>
    public void Die()
    {
        StartCoroutine(DeathSequence());
    }

    private IEnumerator DeathSequence()
    {
        // Fade the CanvasGroup alpha from 0 → 1 over fadeDuration
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            deathScreenGroup.alpha = Mathf.Clamp01(timer / fadeDuration);
            yield return null;
        }

        // Ensure fully opaque
        deathScreenGroup.alpha = 1f;

        // Now show the red text
        deathText.SetActive(true);
    }
}
