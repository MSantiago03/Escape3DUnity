using UnityEngine;

public class StatueTrigger : MonoBehaviour
{
    public StatueController statueController;
    public float moveDistanceX; // Additional X distance to move
    public float moveDistanceZ; // Additional Z distance to move
    public float moveDuration;
    public float returnDelay;
    public GameObject ghostTrigger;

    private void Start()
    {
        ghostTrigger.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Statue"))
        {
            if (gameObject.name != "PedestalYes")
            {
                ghostTrigger.SetActive(true);

                // Schedule the destruction of this gameObject after 1 second
                Invoke("DestroyThisGameObject", 18f);
            }
            else
            {
                Renderer parentRenderer = transform.parent.GetComponent<Renderer>();
                if (parentRenderer != null)
                {
                    parentRenderer.material.color = Color.green;
                }
            }

            statueController.MoveStatueWithInput(
                moveDistanceX,
                moveDistanceZ,
                moveDuration,
                returnDelay
            );
        }
    }

    // Function to destroy the gameObject
    private void DestroyThisGameObject()
    {
        Destroy(gameObject);
        Destroy(ghostTrigger);
    }
}
