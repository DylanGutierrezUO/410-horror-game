using UnityEngine;

public class HighlightInteractable : MonoBehaviour
{
    private Renderer objRenderer;
    private Color originalColor;
    public Color highlightColor = Color.yellow;

    void Start()
    {
        objRenderer = GetComponent<Renderer>();
        if (objRenderer != null)
        {
            originalColor = objRenderer.material.color;
        }
    }

    public void Highlight()
    {
        if (objRenderer != null)
        {
            objRenderer.material.color = highlightColor;
        }
    }

    public void Unhighlight()
    {
        if (objRenderer != null)
        {
            objRenderer.material.color = originalColor;
        }
    }
}
