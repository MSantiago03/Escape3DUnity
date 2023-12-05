using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField]
    private Door Door1;
    [SerializeField]
    private Door Door2 = null;
    [SerializeField]
    int keyReq = 0;

    private void OnTriggerEnter(Collider other)
    {
        // Object that enters the collider must be player to open door
        if (other.gameObject.CompareTag("Player"))
        {
            // Doors must be closed and player count must be greater than 0
            if (!Door1.IsOpen && GameVariables.keyCount >= keyReq)
            {
                Door1.Open(other.transform.position);
                if (Door2 != null)
                {
                    Door2.Open(other.transform.position); 
                }

            }
        }
    }

}