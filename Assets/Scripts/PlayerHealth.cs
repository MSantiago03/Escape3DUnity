using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public GameObject heart4;

    
    public void updateHearts()
    {
        if (GameVariables.lives == 4)
        {
            heart1.SetActive(false);
        }
        else if (GameVariables.lives == 3)
        {
            heart2.SetActive(false);
        }
        else if (GameVariables.lives == 2)
        {
            heart3.SetActive(false);
        }
        else if (GameVariables.lives == 1)
        {
            heart4.SetActive(false);
        }
        else if (GameVariables.lives <= 0)
        {
            SceneManager.LoadScene("Prison");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            updateHearts();
        }
    }
}
