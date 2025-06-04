using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

public class SwitchActivator : MonoBehaviour, IActivatable
{
  // this string shows up in the prompt
  public string InteractionPrompt => "Press E";

  public UnityEvent OnActivated;
  [SerializeField] private AudioSource audioSource; // Reference to AudioSource
  [SerializeField] private AudioClip activationSound; // Sound to play

    // PlayerInteraction will call this:
  public void OnActivate()
  {
        if (audioSource && activationSound)
        {
            GameObject tempGO = new GameObject("TempAudio");
            tempGO.transform.position = transform.position;

            AudioSource tempAudio = tempGO.AddComponent<AudioSource>();
            tempAudio.clip = activationSound;
            tempAudio.volume = 0.125f;  
            tempAudio.Play();

            Destroy(tempGO, activationSound.length);
        }

        OnActivated.Invoke();
  }
}
