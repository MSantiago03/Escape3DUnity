using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : MonoBehaviour
{

    public 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.E))
    //    {
    //        TryGatherTotem();
    //    }
    //}

    //private void TryGatherTotem()
    //{
    //    RaycastHit hit;

    //    // Check if the raycast hits an object within the pickUpDistance
    //    if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out hit, pickUpDistance))
    //    {
    //        // Check if the hit object is the start button
    //        if (hit.collider.gameObject == startButton & startDisable == false)
    //        {
    //            // If the hit object is the start button, start the game
    //            StartGame();
    //        }
    //        if (hit.collider.CompareTag("SimonButton") && buttonsInteractable)
    //        {

    //            Debug.Log("Clicked on another button");
    //            // If the hit object is one of the buttons, simulate a click
    //            GameObject hitObject = hit.collider.gameObject;
    //            int buttonIndex = buttons.IndexOf(hitObject);


    //            Debug.Log("index is" + buttonIndex);

    //            if (buttonIndex != -1)
    //            {
    //                CreatePlayerList(buttonIndex);
    //            }
    //        }
    //    }
    //}
}
