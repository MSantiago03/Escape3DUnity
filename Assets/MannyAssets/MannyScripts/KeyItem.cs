using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code for key objects
// This should update when the user interacts with a key
public class KeyItem : MonoBehaviour
{

    public GameObject key1;
    public GameObject key2;
    public GameObject key3;
    public GameObject key4;

    public void Take()
    {
        GameVariables.keyCount++;
        Destroy(gameObject);
        if(GameVariables.keyCount == 1)
        {
            key1.SetActive(true);
        }
        else if (GameVariables.keyCount == 2)
        {
            key2.SetActive(true);
        }
        else if (GameVariables.keyCount == 3)
        {
            key3.SetActive(true);
        }
        else if (GameVariables.keyCount == 4)
        {
            key4.SetActive(true);
        }
    }

}
