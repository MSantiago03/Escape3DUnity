using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableObject : MonoBehaviour
{
    public Material highlightMaterial;

    private Material originalMaterialHighlight;
    private Transform highlight;
    private float pickUpDistance = 4f;

    [SerializeField]
    private Transform playerCamera1;

    void Update()
    {
        // Highlight
        if (highlight != null)
        {
            highlight.GetComponent<MeshRenderer>().sharedMaterial = originalMaterialHighlight;
            highlight = null;
        }

        RaycastHit hit;

        if (Physics.Raycast(playerCamera1.position, playerCamera1.forward, out hit, pickUpDistance))
        {
            highlight = hit.transform;
            if (highlight.GetComponent<SelectableObject>() != null)
            {
                if (highlight.GetComponent<MeshRenderer>().material != highlightMaterial)
                {
                    originalMaterialHighlight = highlight.GetComponent<MeshRenderer>().material;
                    highlight.GetComponent<MeshRenderer>().material = highlightMaterial;
                }
            }
            else
            {
                highlight = null;
            }
        }
    }
}