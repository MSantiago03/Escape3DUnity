using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projector : MonoBehaviour
{
    public GameObject projection;
    private bool soundPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the sound has not been played yet
        if (!soundPlayed)
        {
            // Check if the triggering object has a specific tag (you can customize this)
            if (other.CompareTag("Player"))
            {
                // Activate the projection and play the sound only for the player
                projection.SetActive(true);

                // Make sure there's an AudioSource component attached to this GameObject
                AudioSource audioSource = GetComponent<AudioSource>();
                if (audioSource != null)
                {
                    audioSource.Play();
                    soundPlayed = true; // Set the flag to true to prevent playing the sound again
                }
            }
        }
    }

}
