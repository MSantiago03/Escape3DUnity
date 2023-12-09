using UnityEngine;

public class PlaySoundOnProximity : MonoBehaviour
{
    public AudioSource audioSource;
    public string playerTag = "Player";
    public float maxDistance = 10f;

    void Update()
    {
        // Find all GameObjects with the specified tag ("Player")
        GameObject[] players = GameObject.FindGameObjectsWithTag(playerTag);

        // Assume there's no player in proximity initially
        bool playerInProximity = false;

        // Check if any player is in proximity
        foreach (GameObject player in players)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (distanceToPlayer <= maxDistance)
            {
                // Player is in proximity, set the volume based on distance
                float volume = Mathf.Clamp01(1f - (distanceToPlayer / maxDistance));
                audioSource.volume = volume;

                playerInProximity = true;
                break; // Exit the loop since we only need to adjust the volume once when a player is in proximity
            }
        }

        // If no player is in proximity, set volume to 0
        if (!playerInProximity)
        {
            audioSource.volume = 0f;
        }

        // Play or stop the audio source based on volume
        if (audioSource.volume > 0 && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
        else if (audioSource.volume == 0 && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}