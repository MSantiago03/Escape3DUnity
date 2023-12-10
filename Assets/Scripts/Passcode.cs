using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Passcode : MonoBehaviour
{
    string Code = "9382";
    string Nr = null;
    public Text UiText = null;
    public GameObject[] chests;
    public float moveDistance = 10.0f;
    public float moveSpeed = 20.0f;
    public GameObject success;

    public void CodeFunction(string Numbers)
    {
        Nr = Nr + Numbers;
        UiText.text = Nr;
    }

    public void Enter()
    {
        if (Nr == Code)
        {
            success.SetActive(true);
            MoveChests();
            UiText.color = Color.green;
        }
        else
        {
            UiText.color = Color.red;
        }
    }

    void MoveChests()
    {
        foreach (var chest in chests)
        {
       
            chest.transform.position = new Vector3(chest.transform.position.x, chest.transform.position.y, chest.transform.position.z + 2.5f);
        }
    }

    public void Delete()
    {
        Nr = null;
        UiText.text = Nr;
        UiText.color = Color.white;
    }
}


