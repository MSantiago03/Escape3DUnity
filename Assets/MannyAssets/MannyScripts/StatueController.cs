using System.Collections;
using UnityEngine;

public class StatueController : MonoBehaviour
{
    private bool isMoving = false;
    private Vector3 initialPosition;
    private float moveDuration;
    public GameObject textBoxPrefab; // Add a reference to your text box prefab

    private void Start()
    {
        InvokeRepeating("GenerateTextBox", 1200f, 1200f); // Invoke GenerateTextBox every 1200 seconds (20 minutes)
    }

    public void MoveStatueWithInput(float addedMoveDistanceX, float addedMoveDistanceZ, float moveDuration, float returnDelay)
    {
        if (!isMoving)
        {
            initialPosition = transform.position; // Store initial position
            this.moveDuration= moveDuration;
            StartCoroutine(MoveStatueCoroutine(addedMoveDistanceX, addedMoveDistanceZ, moveDuration, returnDelay));
        }

    }

    private IEnumerator MoveStatueCoroutine(float addedMoveDistanceX, float addedMoveDistanceZ, float moveDuration, float returnDelay)
    {
        isMoving = true;
        float elapsedTime = 0f;
        Vector3 targetPosition = initialPosition + new Vector3(addedMoveDistanceX, 0f, addedMoveDistanceZ);

        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        yield return new WaitForSeconds(returnDelay);
        StartCoroutine(ReturnToOriginalPosition());
    }

    private IEnumerator ReturnToOriginalPosition()
    {
        float elapsedTime = 0f;
        Vector3 targetPosition = initialPosition;

        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = initialPosition; // Ensure it reaches exactly the initial position
        isMoving = false;
    }

    private void GenerateTextBox()
    {
        textBoxPrefab.SetActive(true);
    }
}
