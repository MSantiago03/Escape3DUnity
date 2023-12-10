using UnityEngine;

public class StatueTrigger : MonoBehaviour
{
    public StatueController statueController;
    public float moveDistanceX; // Additional X distance to move
    public float moveDistanceZ; // Additional Z distance to move
    public float moveDuration;
    public float returnDelay;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Statue"))
        {
            statueController.MoveStatueWithInput(
                moveDistanceX,
                moveDistanceZ,
                moveDuration,
                returnDelay
            );
        }
    }
}
