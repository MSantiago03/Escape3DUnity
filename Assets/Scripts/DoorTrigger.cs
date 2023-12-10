using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    // Reference to the first door affected by the trigger
    [SerializeField] private Door Door1;

    // Reference to the optional second door affected by the trigger
    [SerializeField] private Door Door2 = null;

    // Number of keys required to open the door
    [SerializeField] private int keyReq = 0;

    // GameObject representing a sign to display a message
    [SerializeField] private GameObject sign;

    // Flag to track whether the trigger has been activated
    private bool notDone = true;

    // Called when the script is first run
    private void Start()
    {
        // Deactivate the sign at the start
        sign.SetActive(false);
    }

    // Called when another collider enters the trigger collider
    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering object is tagged as "Player" and the trigger has not been activated
        if (other.gameObject.CompareTag("Player") && notDone)
        {
            // Check if the first door is closed and the player has enough keys
            if (!Door1.IsOpen && GameVariables.keyCount >= keyReq)
            {
                // Open the first door
                Door1.Open();

                // If there is a second door, open it as well
                if (Door2 != null)
                {
                    Door2.Open();
                }

                // Set the trigger as done to prevent further activation
                notDone = false;

                // Destroy the associated sign GameObject
                Destroy(sign);
            }
            else
            {
                // Display the sign if the conditions for opening the door are not met
                sign.SetActive(true);
            }
        }
    }
}
