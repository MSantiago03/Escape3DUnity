using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirectionList : MonoBehaviour
{

    void Start()
    {
        // Ensure that the Text component is assigned in the Unity Editor
        // Attach the ClickHandler method to the button's onClick event
        //GetComponent<Button>().onClick.AddListener(ClickHandler);
    }

    public void ClickHandler()
    {
        // Toggle the visibility of the Text element
        gameObject.SetActive(!gameObject.activeSelf);

        // Optionally, you can set the text content here as well
        // displayText.text = "Button Clicked!";
    }

}
