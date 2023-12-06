using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Quit : MonoBehaviour
{


    public void QuitGame()
    {
        SceneManager.LoadScene("OpeningScene");
    }
}