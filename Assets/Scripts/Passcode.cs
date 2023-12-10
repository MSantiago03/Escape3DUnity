using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Passcode : MonoBehaviour
{
    // Correct passcode
    string Code = "9382";

    // Current inputted passcode
    string Nr = null;

    // UI Text element to display the passcode
    public Text UiText = null;

    // Array of chests affected by the passcode
    public GameObject[] chests;

    // Distance to move the chests when the correct passcode is entered
    public float moveDistance = 10.0f;

    // Speed at which the chests move
    public float moveSpeed = 20.0f;

    // GameObject representing success message
    public GameObject success;

    // Method to update the entered passcode
    public void CodeFunction(string Numbers)
    {
        Nr = Nr + Numbers;
        UiText.text = Nr;
    }

    // Method to check and process the entered passcode
    public void Enter()
    {
        // Check if the entered passcode is correct
        if (Nr == Code)
        {
            // Activate success message and move the chests
            success.SetActive(true);
            MoveChests();

            // Change UI text color to green
            UiText.color = Color.green;
        }
        else
        {
            // Change UI text color to red for incorrect passcode
            UiText.color = Color.red;
        }
    }

    // Method to move the chests
    void MoveChests()
    {
        // Move each chest in the array
        foreach (var chest in chests)
        {
            // Update the chest's position along the z-axis
            chest.transform.position = new Vector3(chest.transform.position.x, chest.transform.position.y, chest.transform.position.z + moveDistance);
        }
    }

    // Method to delete the entered passcode
    public void Delete()
    {
        Nr = null;
        UiText.text = Nr;

        // Reset UI text color to white
        UiText.color = Color.white;
    }
}

