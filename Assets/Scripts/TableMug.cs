using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableMug : MonoBehaviour
{
    // Reference to the letter object
    public GameObject letter;

    // Reference to the text box prefab
    public GameObject textBoxPrefab;

    // Flags to track the presence of each mug
    private bool mug1 = false;
    private bool mug2 = false;
    private bool mug3 = false;
    private bool mug4 = false;

    // Counter for the number of mugs on the table
    private int mugCount = 0;

    // Called when the script is first run
    private void Start()
    {
        // Deactivate the letter object at the start
        letter.SetActive(false);

        // Invoke the GenerateTextBox method every 300 seconds (5 minutes)
        InvokeRepeating("GenerateTextBox", 300f, 300f);
    }

    // Called when another collider enters the trigger collider attached to this object
    private void OnTriggerEnter(Collider other)
    {
        // Check the tag of the entering object and update the corresponding mug flag
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

        // Check if all four mugs are on the table
        if (mugCount == 4)
        {
            // Activate the letter object and deactivate the text box prefab
            letter.SetActive(true);
            textBoxPrefab.SetActive(false);
        }
    }

    // Method to generate a text box
    private void GenerateTextBox()
    {
        // Check if not all four mugs are on the table
        if (mugCount < 4)
        {
            // Activate the text box prefab
            textBoxPrefab.SetActive(true);
        }
    }
}