using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    [SerializeField]
    private Transform playerCameraTransform;
    [SerializeField]
    private Transform objectGrabPointTransform;
    [SerializeField]
    private LayerMask pickUpLayerMask;
    [SerializeField]
    private float pickUpDistance = 4f;

    private ObjectGrabbable objectGrabbable;
    private DrawerController drawerController;
    private KeyItem keyItem;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {

            if (objectGrabbable == null)
            {
                if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickUpDistance))
                {

                    if (raycastHit.transform.TryGetComponent(out objectGrabbable))
                    {
                        objectGrabbable.Grab(objectGrabPointTransform);
                        Debug.Log(objectGrabbable);
                        Debug.Log("Object being picked up");


                    } else if (raycastHit.transform.TryGetComponent(out keyItem))
                    {
                        Debug.Log("Raycast has hit key");
                        keyItem.Take();
                    }
                }

            } else
            {
                Debug.Log("Object being released!");
                objectGrabbable.Drop();
                objectGrabbable = null;
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickUpDistance))
            {
                if (raycastHit.transform.TryGetComponent(out drawerController))
                {
                    if (!drawerController.IsOpen)
                    {
                        drawerController.Pull();
                    } else
                    {
                        drawerController.Close();
                    }
                }
            }
        }

    }

}