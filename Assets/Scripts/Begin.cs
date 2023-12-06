using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Begin : MonoBehaviour
{
    private MusicController controller;

    void Start()
    {
        controller = GetComponent<MusicController>();
    }

    public void LoadNewScene()
    {
        SceneManager.LoadScene("Prison");
        controller.stopMusic();
    }
}