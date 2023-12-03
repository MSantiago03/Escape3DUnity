using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField]
    private Door DoorLeft;
    [SerializeField]
    private Door DoorRight;

    private void OnTriggerEnter(Collider other)
    {
        // Object that enters the collider must be player to open door
        if (other.gameObject.CompareTag("Player"))
        {
            // Doors must be closed and player count must be greater than 0
            if (!DoorLeft.IsOpen && !DoorRight.IsOpen && GameVariables.keyCount > 0)
            {
                DoorLeft.Open(other.transform.position);
                DoorRight.Open(other.transform.position);

                GameVariables.keyCount--;
            }
        }
    }

}