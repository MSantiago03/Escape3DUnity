using UnityEngine;
using UnityEngine.UI;

public class Passcode : MonoBehaviour
{
    string Code = "123";
    string Nr = null;
    public Text UiText = null;
    public GameObject[] chests;
    public float moveDistance = 10.0f;
    public float moveSpeed = 20.0f;

    public void CodeFunction(string Numbers)
    {
        Nr = Nr + Numbers;
        UiText.text = Nr;
    }

    public void Enter()
    {
        if (Nr == Code)
        {
            print("Success");
            MoveChests();
        }
        else
        {
            print("Incorrect code. Try again.");
        }
    }

    void MoveChests()
    {
        foreach (var chest in chests)
        {
            print("moving");
            chest.transform.position = new Vector3(chest.transform.position.x, chest.transform.position.y, -9.0f);
            print($"New Position: {chest.transform.position}");
        }
    }

    public void Delete()
    {
        Nr = null;
        UiText.text = Nr;
    }
}


