using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "player")
        {
            GameVariables.keyCount += 1;
            Destroy(gameObject);
        }
    }
}
