using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabbable : MonoBehaviour
{
    // Rigidbody of the grabbable object
    private Rigidbody objectRigidbody;

    // Transform representing the point where the object is being grabbed
    private Transform objectGrabPointTransform;

    // Called when the script is first run
    private void Awake()
    {
        // Get the Rigidbody component attached to the object
        objectRigidbody = GetComponent<Rigidbody>();
    }

    // Method to grab the object
    public void Grab(Transform objectGrabPointTransform)
    {
        // Set the grab point and disable gravity while grabbed
        this.objectGrabPointTransform = objectGrabPointTransform;
        objectRigidbody.useGravity = false;
    }

    // Method to drop the object
    public void Drop()
    {
        // Clear the grab point and enable gravity
        this.objectGrabPointTransform = null;
        objectRigidbody.useGravity = true;
    }

    // Called at a fixed rate, typically used for physics calculations
    private void FixedUpdate()
    {
        // Check if the object is currently being grabbed
        if (objectGrabPointTransform != null)
        {
            // Set the speed for lerping (linear interpolation) the object to the grab point
            float lerpSpeed = 10f;

            // Calculate the new position using Lerp
            Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);

            // Move the object to the new position using Rigidbody.MovePosition
            objectRigidbody.MovePosition(newPosition);
        }
    }
}

