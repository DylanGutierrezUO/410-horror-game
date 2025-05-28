using UnityEngine;
using UnityEngine.Events;

public class SwitchActivator : MonoBehaviour, IActivatable
{
  // this string shows up in the prompt
  public string InteractionPrompt => "Press E";

  public UnityEvent OnActivated;

  // PlayerInteraction will call this:
  public void OnActivate()
  {
    OnActivated.Invoke();
  }
}
