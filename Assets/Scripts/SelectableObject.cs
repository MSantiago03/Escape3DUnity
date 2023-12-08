using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableObject : MonoBehaviour
{
    public Material highlightMaterial;

    private Material originalMaterialHighlight;
    private Transform lastHighlightedObject;
    private float pickUpDistance = 4f;

    [SerializeField]
    private Transform playerCamera1;

    void Update()
    {
        // Revert the material for the last highlighted object
        if (lastHighlightedObject != null)
        {
            Renderer renderer = lastHighlightedObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = originalMaterialHighlight;
            }
            lastHighlightedObject = null;
        }

        RaycastHit hit;

        if (Physics.Raycast(playerCamera1.position, playerCamera1.forward, out hit, pickUpDistance))
        {
            Transform newHighlight = hit.transform;

            if (newHighlight.GetComponent<SelectableObject>() != null)
            {
                if (newHighlight.GetComponent<MeshRenderer>().material != highlightMaterial)
                {
                    originalMaterialHighlight = newHighlight.GetComponent<MeshRenderer>().material;
                    newHighlight.GetComponent<MeshRenderer>().material = highlightMaterial;

                    // Update the last highlighted object
                    lastHighlightedObject = newHighlight;
                }
            }
        }
    }
}