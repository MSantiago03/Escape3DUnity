using System.Collections;
using UnityEngine;

public class StatueController : MonoBehaviour
{
    // Flag indicating whether the statue is currently moving
    private bool isMoving = false;

    // Initial position of the statue
    private Vector3 initialPosition;

    // Duration of the statue movement
    private float moveDuration;

    // Prefab for text box
    public GameObject textBoxPrefab;

    // Called when the script is first run
    private void Start()
    {
        // Invoke the GenerateTextBox method every 600 seconds (10 minutes)
        InvokeRepeating("GenerateTextBox", 600f, 600f);
    }

    // Method to move the statue with input parameters
    public void MoveStatueWithInput(float addedMoveDistanceX, float addedMoveDistanceZ, float moveDuration, float returnDelay)
    {
        // Check if the statue is not currently moving
        if (!isMoving)
        {
            // Store the initial position of the statue
            initialPosition = transform.position;

            // Store the specified move duration
            this.moveDuration = moveDuration;

            // Start the coroutine to move the statue
            StartCoroutine(MoveStatueCoroutine(addedMoveDistanceX, addedMoveDistanceZ, moveDuration, returnDelay));
        }
    }

    // Coroutine to move the statue to a new position
    private IEnumerator MoveStatueCoroutine(float addedMoveDistanceX, float addedMoveDistanceZ, float moveDuration, float returnDelay)
    {
        // Set the statue as currently moving
        isMoving = true;

        // Initialize elapsed time
        float elapsedTime = 0f;

        // Calculate the target position based on added move distances
        Vector3 targetPosition = initialPosition + new Vector3(addedMoveDistanceX, 0f, addedMoveDistanceZ);

        // Move the statue using Lerp over the specified duration
        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the statue reaches exactly the target position
        transform.position = targetPosition;

        // Wait for the specified return delay before returning to the original position
        yield return new WaitForSeconds(returnDelay);

        // Start the coroutine to return the statue to the original position
        StartCoroutine(ReturnToOriginalPosition());

        // Deactivate the text box prefab
        textBoxPrefab.SetActive(false);
    }

    // Coroutine to return the statue to its original position
    private IEnumerator ReturnToOriginalPosition()
    {
        // Initialize elapsed time
        float elapsedTime = 0f;

        // Target position is the initial position
        Vector3 targetPosition = initialPosition;

        // Move the statue back to the initial position using Lerp over the specified duration
        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the statue reaches exactly the initial position
        transform.position = initialPosition;

        // Set the statue as not moving
        isMoving = false;
    }

    // Method to generate a text box
    private void GenerateTextBox()
    {
        // Activate the text box prefab
        textBoxPrefab.SetActive(true);
    }
}
