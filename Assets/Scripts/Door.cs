using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Flag to track if the door is open
    public bool IsOpen = false;

    // Speed of the door animation
    [SerializeField] private float Speed = 1f;

    // Rotation amount for the door when opening
    [Header("Rotation Configs")]
    [SerializeField] private float RotationAmount = 90f;

    // Sound to play when the door opens
    [Header("Audio")]
    [SerializeField] private AudioClip doorOpenSound;

    // Initial rotation of the door
    private Vector3 StartRotation;

    // Coroutine for handling the door animation
    private Coroutine AnimationCoroutine;

    // AudioSource component for playing sounds
    private AudioSource audioSource;

    private void Awake()
    {
        // Save the initial rotation of the door
        StartRotation = transform.rotation.eulerAngles;

        // Get or add an AudioSource component to the GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Method to open the door
    public void Open()
    {
        // Check if the door is not already open
        if (!IsOpen)
        {
            // Stop any existing door animation coroutine
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }

            // Start a new coroutine to animate the door open
            AnimationCoroutine = StartCoroutine(DoRotationOpen(RotationAmount));

            // Play the door opening sound if available
            if (doorOpenSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(doorOpenSound);
            }
        }
    }

    // Coroutine to handle the rotation animation of the door
    private IEnumerator DoRotationOpen(float rotationInput)
    {
        // Save the start and end rotations
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(new Vector3(StartRotation.x, StartRotation.y + rotationInput, StartRotation.z));

        // Set the door as open
        IsOpen = true;

        // Animation loop
        float time = 0;
        while (time < 1)
        {
            // Slerp (spherical linear interpolation) to smoothly interpolate between start and end rotations
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);

            // Wait for the next frame
            yield return null;

            // Update time based on animation speed
            time += Time.deltaTime * Speed;
        }
    }
}