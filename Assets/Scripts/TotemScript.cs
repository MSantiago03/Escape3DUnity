using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TotemScript : MonoBehaviour
{

    [SerializeField]
    private Transform playerCameraTransform;

    public GameObject number4;

    private float pickUpDistance = 4f;

    public TextMeshProUGUI totemText;

    private int numTotem = 0;

    private Vector3 lastDestroyedTotemPosition;


    // Start is called before the first frame update
    void Start()
    {
        // Deactivate number4 initially
        number4.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryGatherTotem();
        }
    }

    private void TryGatherTotem()
    {
        RaycastHit hit;

        // Check if the raycast hits an object within the pickUpDistance
        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out hit, pickUpDistance))
        {
            // Check if the hit object is the start button
            if (hit.collider.CompareTag("Totem"))
            {
                // If the hit object is the start button, start the game
                numTotem += 1;
                totemText.text = numTotem.ToString();
                lastDestroyedTotemPosition = hit.collider.transform.position;
                Destroy(hit.collider.gameObject);
            }

            if (numTotem == 4)
            {
                SpawnNumber();
            }


        }
    }

    private void SpawnNumber()
    {
        lastDestroyedTotemPosition = lastDestroyedTotemPosition + new Vector3(0, 2, 0);
        number4.SetActive(true);
        number4.transform.position = lastDestroyedTotemPosition;
    }
}
