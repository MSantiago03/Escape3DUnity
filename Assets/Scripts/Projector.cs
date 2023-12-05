using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projector : MonoBehaviour
{
    public GameObject projection;

    private void OnTriggerEnter(Collider other)
    {
        projection.SetActive(true);
    }
}
