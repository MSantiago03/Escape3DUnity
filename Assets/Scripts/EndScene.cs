using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Resetting vars for new game
        GameVariables.lives = 4;
        GameVariables.keyCount = 0;
        SceneManager.LoadScene("EndScene");
    }
}