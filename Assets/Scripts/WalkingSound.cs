using UnityEngine;

public class WalkingSound : MonoBehaviour
{
    public AudioSource footstepAudio;
    public SC_FPSController fpsController;

    public float footstepDelay = 0.5f;
    private float footstepTimer;

    void Update()
    {
        // Calculate horizontal speed only
        Vector3 horizontalVelocity = new Vector3(
            fpsController.characterController.velocity.x,
            0,
            fpsController.characterController.velocity.z
        );

        // Check if the player is grounded and moving horizontally
        if (fpsController.characterController.isGrounded && horizontalVelocity.magnitude > 0.1f)
        {
            footstepTimer -= Time.deltaTime;
            if (footstepTimer <= 0f)
            {
                if (!footstepAudio.isPlaying)  // Play only if not already playing
                {
                    footstepAudio.Play();
                }
                footstepTimer = footstepDelay;
            }
        }
        else
        {
            // Stop the footstep sound when not moving or not grounded
            if (footstepAudio.isPlaying)
            {
                footstepAudio.Stop();
            }
            footstepTimer = 0f;
        }
    }
}
