using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code for key objects
// This should update when the user interacts with a key
public class KeyItem : MonoBehaviour
{

    public void Take()
    {
        GameVariables.keyCount++;
        Destroy(gameObject);
    }

}
