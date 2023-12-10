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

    [Header("Audio")]
    [SerializeField]
    public AudioClip pickUpSound;
    private AudioSource audioSource;

    private void Start()
    {
        // Add an AudioSource component to the same GameObject as this script
        audioSource = gameObject.AddComponent<AudioSource>();
    }

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
                        audioSource.PlayOneShot(pickUpSound);
                    }
                    else if (raycastHit.transform.TryGetComponent(out keyItem))
                    {
                        keyItem.Take();
                        GetComponent<AudioSource>().Play();
                        audioSource.PlayOneShot(pickUpSound);
                    }
                }
            }
            else
            {
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
                    }
                    else
                    {
                        drawerController.Close();
                    }
                }
            }
        }
    }
}