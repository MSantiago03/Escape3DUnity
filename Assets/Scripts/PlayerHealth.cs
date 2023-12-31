using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public GameObject heart4;

    private void Update()
    {
        if (GameVariables.lives == 3)
        {
            heart1.SetActive(false);
        }
        else if (GameVariables.lives == 2)
        {
            heart2.SetActive(false);
        }
        else if (GameVariables.lives == 1)
        {
            heart3.SetActive(false);
        }
        else if (GameVariables.lives <= 0)
        {
            SceneManager.LoadScene("Prison");
            GameVariables.lives = 4;
        }
    }

}