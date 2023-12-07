using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableMug : MonoBehaviour
{
    public GameObject letter;
    public GameObject textBoxPrefab; // Add a reference to your text box prefab
    private bool mug1 = false;
    private bool mug2 = false;
    private bool mug3 = false;
    private bool mug4 = false;

    private int mugCount = 0;

    private void Start()
    {
        InvokeRepeating("GenerateTextBox", 5f, 5f); // Invoke GenerateTextBox every 600 seconds (10 minutes)
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Mug1") && !mug1)
        {
            mug1 = true;
            mugCount++;
        }
        else if (other.gameObject.CompareTag("Mug2") && !mug2)
        {
            mug2 = true;
            mugCount++;
        }
        else if (other.gameObject.CompareTag("Mug3") && !mug3)
        {
            mug3 = true;
            mugCount++;
        }
        else if (other.gameObject.CompareTag("Mug4") && !mug4)
        {
            mug4 = true;
            mugCount++;
        }

        if (mugCount == 4)
        {
            letter.SetActive(true);
        }
    }

    private void GenerateTextBox()
    {
        if (mugCount < 4)
        {
            // Generate a new text box
            textBoxPrefab.SetActive(true);
        }
    }
}
