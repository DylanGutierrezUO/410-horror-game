using UnityEngine;

public class KeyPickup : Interactable
{
    [SerializeField] private AudioSource audioSource; 
    [SerializeField] private AudioClip pickupSound;    

    // This is what the player will see
    public override string InteractionPrompt => "Press E";

    public override void OnFocus()
    {
        Debug.Log("Focus on key. Press E to pick up.");
    }

    public override void OnDefocus()
    {
        Debug.Log("Defocus from key.");
    }

    public override void Interact()
    {
        if (pickupSound)
        {
            GameObject tempGO = new GameObject("TempAudio");
            tempGO.transform.position = transform.position;

            AudioSource tempAudio = tempGO.AddComponent<AudioSource>();
            tempAudio.clip = pickupSound;
            tempAudio.volume = 0.2f;  // Adjust volume between 0.0 (silent) and 1.0 (full volume)
            tempAudio.Play();

            // Destroy after clip duration
            Destroy(tempGO, pickupSound.length);
        }
        Debug.Log("Picked up key!");

        Inventory.Instance.CollectKey();
        Destroy(gameObject);
    }
}