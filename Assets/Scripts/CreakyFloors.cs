using UnityEngine;

public class CreakyFloor : MonoBehaviour
{
    public AudioClip[] creakSounds;  
    public AudioSource audioSource;

    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered by: " + other.name);
        // Check if the object stepping on it is a player or monster
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            Debug.Log("Tag matched. Playing sound.");
            if (!audioSource.isPlaying)
            {
                int randomIndex = Random.Range(0, creakSounds.Length);
                audioSource.clip = creakSounds[randomIndex];
                audioSource.Play();
            }
        }
    }
}
