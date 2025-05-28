public interface IActivatable
{
    /// <summary>
    /// Shown on screen when the player looks at this object.
    /// </summary>
    string InteractionPrompt { get; }

    /// <summary>
    /// Called by PlayerInteraction when the player presses E.
    /// </summary>
    void OnActivate();
}
