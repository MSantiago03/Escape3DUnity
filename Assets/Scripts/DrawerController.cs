using System.Collections;
using UnityEngine;

public class DrawerController : MonoBehaviour
{
    // Speed at which the drawer opens or closes
    [SerializeField] private float Speed = 1f;

    // Pull distance for the drawer to open
    [Header("Pulling Configs")]
    [SerializeField] private float PullDistance = 1f;

    // Initial position of the drawer
    private Vector3 StartPosition;

    // End position when the drawer is pulled
    private Vector3 EndPosition;

    // Coroutine for handling the drawer animation
    private Coroutine AnimationCoroutine;

    // Flag to track whether the drawer is open
    public bool IsOpen = false;

    // Called when the script is first run
    private void Awake()
    {
        // Set the start and end positions based on the pull distance
        StartPosition = transform.position;
        EndPosition = StartPosition + transform.forward * PullDistance;

        // If the drawer should start open, set its position to EndPosition
        if (IsOpen)
        {
            transform.position = EndPosition;
        }
    }

    // Method to pull the drawer open
    public void Pull()
    {
        // Check if the drawer is not already open
        if (!IsOpen)
        {
            // Stop any existing drawer animation coroutine
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }

            // Start a new coroutine to animate the drawer open
            AnimationCoroutine = StartCoroutine(AnimatePull());
        }
    }

    // Method to close the drawer
    public void Close()
    {
        // Check if the drawer is open
        if (IsOpen)
        {
            // Stop any existing drawer animation coroutine
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }

            // Start a new coroutine to animate the drawer close
            AnimationCoroutine = StartCoroutine(AnimateClose());
        }
    }

    // Coroutine to handle the drawer pull animation
    private IEnumerator AnimatePull()
    {
        // Set the drawer as open
        IsOpen = true;

        // Animation loop
        float time = 0;
        Vector3 startPosition = transform.position;

        while (time < 1)
        {
            // Lerp (linear interpolation) to smoothly interpolate between start and end positions
            transform.position = Vector3.Lerp(startPosition, EndPosition, time);

            // Wait for the next frame
            yield return null;

            // Update time based on animation speed
            time += Time.deltaTime * Speed;
        }

        // Ensure the drawer is at the end position
        transform.position = EndPosition;
    }

    // Coroutine to handle the drawer close animation
    private IEnumerator AnimateClose()
    {
        // Set the drawer as closed
        IsOpen = false;

        // Animation loop
        float time = 0;
        Vector3 endPosition = StartPosition;

        while (time < 1)
        {
            // Lerp (linear interpolation) to smoothly interpolate between start and end positions
            transform.position = Vector3.Lerp(EndPosition, endPosition, time);

            // Wait for the next frame
            yield return null;

            // Update time based on animation speed
            time += Time.deltaTime * Speed;
        }

        // Ensure the drawer is at the end position
        transform.position = endPosition;
    }
}
