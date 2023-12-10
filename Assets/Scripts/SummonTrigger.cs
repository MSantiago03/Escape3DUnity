using UnityEngine;

public class SummonTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject ghost;
    [SerializeField]
    private float presenceLength = 5f; // Set the default presence length to 5 seconds


    //public AudioSource audioSource;

    private bool ghostIsPresent = false;
    private Vector3 originalPosition;
    private float timer = 0f;

    private void Start()
    {
        // Store the original position of the ghost
        originalPosition = ghost.transform.position;
        // Hide the ghost initially
        ghost.SetActive(false);
        
    }

    private void Update()
    {
        // Check if the ghost is present and the timer is running
        if (ghostIsPresent)
        {
            timer += Time.deltaTime;
            // If the timer exceeds the presence length, reset the ghost and timer
            if (timer >= presenceLength)
            {
                // Timer has run out, reseting ghost, and stopping music
                ghost.GetComponent<GhostControllerAi>().isSummoned = false;
                ghost.GetComponent<GhostControllerAi>().isMusicOn = false;
                ghost.GetComponent<GhostControllerAi>().isFrozen = false;
                ResetGhost();
                
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider is the player
        if (other.CompareTag("Player")) 
        {
            // Activating ghost movement
            ghost.GetComponent<GhostControllerAi>().isSummoned = true;
            
            // Show the ghost, start the timer, play music
            ghost.SetActive(true);
            ghostIsPresent = true;
            timer = 0f;
            //audioSource = GetComponent<AudioSource>();
            //audioSource.Play();
        }
    }

    private void ResetGhost()
    {
        // Reset the ghost to its original position and hide it
        ghost.transform.position = originalPosition;
        ghost.SetActive(false);
        // Reset variables
        ghostIsPresent = false;
        timer = 0f;
    }
}
