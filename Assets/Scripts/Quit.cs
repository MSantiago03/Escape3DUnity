using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Quit : MonoBehaviour
{

    void Start()
    {
        // Ensure that the Text component is assigned in the Unity Editor
        // Attach the ClickHandler method to the button's onClick event
        //GetComponent<Button>().onClick.AddListener(QuitGame);
    }

    public void QuitGame()
    {
        Application.Quit();
        SceneManager.LoadScene("OpeningScene");
    }
}