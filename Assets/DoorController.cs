using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    public GameObject door;

    public float openRotation, closedRotation, speed;

    public bool opening;

    // Update is called once per frame
    void Update()
    {
        Vector3 currRotation = door.transform.localEulerAngles;
        if (opening)
        {
            if (currRotation.y < openRotation)
            {
                door.transform.localEulerAngles = Vector3.Lerp(currRotation, new Vector3(currRotation.x, openRotation, currRotation.z), speed * Time.deltaTime);
            }
        }
        else
        {
            if (currRotation.y > closedRotation)
            {
                door.transform.localEulerAngles = Vector3.Lerp(currRotation, new Vector3(currRotation.x, closedRotation, currRotation.z), speed * Time.deltaTime);
            }
        }
    }

}
