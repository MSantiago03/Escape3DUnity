using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code for key objects
// This should update when the user interacts with a key
public class KeyItem : MonoBehaviour
{
    // When the key item is found
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Increase key amount in GameVariables by one and destroy key
            GameVariables.keyCount += 1;
            Destroy(gameObject);
        }
    }
}
