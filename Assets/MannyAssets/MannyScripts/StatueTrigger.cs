using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    public StatueController statueController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Statue"))
        {
            statueController.MoveStatue();
        }
    }
}
