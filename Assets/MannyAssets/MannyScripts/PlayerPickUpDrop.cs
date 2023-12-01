using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private LayerMask pickUpLayerMask;

    private ObjectGrabbable objectGrabbable;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (objectGrabbable == null)
            {
                float pickUpDistance = 6f;
                if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickUpDistance))
                {
                    Debug.DrawLine(playerCameraTransform.position,  playerCameraTransform.position + playerCameraTransform.forward, Color.green);

                    if (raycastHit.transform.TryGetComponent(out objectGrabbable))
                    {
                        objectGrabbable.Grab(objectGrabPointTransform);
                        Debug.Log(objectGrabbable);
                        Debug.Log("Object being picked up");


                    }
                }
            } else
            {
                Debug.Log("Object being released!");
                objectGrabbable.Drop();
                objectGrabbable = null;
            }
        }

    }

}