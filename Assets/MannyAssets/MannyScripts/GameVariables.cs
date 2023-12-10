using UnityEngine;

public class GameVariables : MonoBehaviour
{
    public static int keyCount = 0;
    public static int lives = 5;


    public static void LoseLife()
    {
        lives--;

    }


}