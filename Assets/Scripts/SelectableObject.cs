using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableObject : MonoBehaviour
{
    // Material to highlight the object
    public Material highlightMaterial;

    // Reference to the original material of the last highlighted object
    private Material originalMaterialHighlight;

    // Transform representing the currently highlighted object
    private Transform newHighlight;

    // Distance for raycasting to detect selectable objects
    private float pickUpDistance = 4f;

    // Reference to the player's camera
    [SerializeField]
    private Transform playerCamera1;

    // Called every frame
    void Update()
    {
        // Revert the material for the last highlighted object
        if (newHighlight != null)
        {
            Renderer renderer = newHighlight.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = originalMaterialHighlight;
                newHighlight = null;
            }
        }

        // Raycast to detect selectable objects
        RaycastHit hit;
        if (Physics.Raycast(playerCamera1.position, playerCamera1.forward, out hit, pickUpDistance))
        {
            // Get the transform of the hit object
            newHighlight = hit.transform;

            // Check if the hit object has an AttachToHighlight component
            if (newHighlight.GetComponent<AttachToHighlight>() != null)
            {
                // Check if the material is not already the highlight material
                if (newHighlight.GetComponent<MeshRenderer>().material != highlightMaterial)
                {
                    // Save the original material, apply the highlight material, and highlight the object
                    originalMaterialHighlight = newHighlight.GetComponent<MeshRenderer>().material;
                    newHighlight.GetComponent<MeshRenderer>().material = highlightMaterial;
                }
            }
            else
            {
                // If the hit object does not have an AttachToHighlight component, set newHighlight to null
                newHighlight = null;
            }
        }
    }
}
