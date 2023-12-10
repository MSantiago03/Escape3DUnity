using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableObject : MonoBehaviour
{
    public Material highlightMaterial;

    private Material originalMaterialHighlight;
    private Transform newHighlight;
    private float pickUpDistance = 4f;

    [SerializeField]
    private Transform playerCamera1;

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

        RaycastHit hit;

        if (Physics.Raycast(playerCamera1.position, playerCamera1.forward, out hit, pickUpDistance))
        {
            newHighlight = hit.transform;

            if (newHighlight.GetComponent<AttachToHighlight>() != null)
            {
                if (newHighlight.GetComponent<MeshRenderer>().material != highlightMaterial)
                {
                    originalMaterialHighlight = newHighlight.GetComponent<MeshRenderer>().material;
                    newHighlight.GetComponent<MeshRenderer>().material = highlightMaterial;

                }
            }
            else
            {
                newHighlight = null;
            }
        }
        
    }
}

