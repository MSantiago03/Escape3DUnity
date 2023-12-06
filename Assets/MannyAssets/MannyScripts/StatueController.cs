using System.Collections;
using UnityEngine;

public class StatueController : MonoBehaviour
{
    [SerializeField]
    private float moveDistance;
    [SerializeField]
    private float moveDuration;
    [SerializeField]
    private float returnDelay; // Delay before returning

    private bool isMoving = false;
    private Vector3 initialPosition;

    public void MoveStatue()
    {
        if (!isMoving)
        {
            initialPosition = transform.position; // Store initial position
            isMoving = true;
            StartCoroutine(MoveX());
        }
    }

    private IEnumerator MoveX()
    {
        float elapsedTime = 0f;
        Vector3 targetPosition = initialPosition + Vector3.right * moveDistance;

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
}
