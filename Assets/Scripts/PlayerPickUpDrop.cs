using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Reference to the player's camera
    [SerializeField] private Transform playerCameraTransform;

    // Reference to the point where objects are grabbed
    [SerializeField] private Transform objectGrabPointTransform;

    // Layer mask to filter objects that can be picked up
    [SerializeField] private LayerMask pickUpLayerMask;

    // Distance for picking up objects
    [SerializeField] private float pickUpDistance = 4f;

    // Reference to the currently grabbed object
    private ObjectGrabbable objectGrabbable;

    // Reference to the DrawerController for handling drawers
    private DrawerController drawerController;

    // Reference to the KeyItem for handling keys
    private KeyItem keyItem;

    // Audio source for playing pickup sounds
    [Header("Audio")]
    [SerializeField] public AudioClip pickUpSound;
    private AudioSource audioSource;

    // Called when the script is first run
    private void Start()
    {
        // Add an AudioSource component to the same GameObject as this script
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check for the E key to pick up or drop objects
        if (Input.GetKeyDown(KeyCode.E))
        {
            // If no object is currently grabbed
            if (objectGrabbable == null)
            {
                // Cast a ray from the player's camera forward to check for grabbable objects
                if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickUpDistance, pickUpLayerMask))
                {
                    // Try to get the ObjectGrabbable component from the hit object
                    if (raycastHit.transform.TryGetComponent(out objectGrabbable))
                    {
                        // Grab the object and play the pickup sound
                        objectGrabbable.Grab(objectGrabPointTransform);
                        audioSource.PlayOneShot(pickUpSound);
                    }
                    // Try to get the KeyItem component from the hit object
                    else if (raycastHit.transform.TryGetComponent(out keyItem))
                    {
                        // Take the key, play the pickup sound, and play a separate sound
                        keyItem.Take();
                        GetComponent<AudioSource>().Play();
                        audioSource.PlayOneShot(pickUpSound);
                    }
                }
            }
            // If an object is currently grabbed, drop it
            else
            {
                objectGrabbable.Drop();
                objectGrabbable = null;
            }
        }

        // Check for the Q key to interact with drawers
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // Cast a ray from the player's camera forward to check for interactable drawers
            if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickUpDistance))
            {
                // Try to get the DrawerController component from the hit object
                if (raycastHit.transform.TryGetComponent(out drawerController))
                {
                    // If the drawer is not open, pull it; otherwise, close it
                    if (!drawerController.IsOpen)
                    {
                        drawerController.Pull();
                    }
                    else
                    {
                        drawerController.Close();
                    }
                }
            }
        }
    }
}
