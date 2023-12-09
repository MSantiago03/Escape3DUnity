using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseGame : MonoBehaviour
{
    public bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public AudioClip pauseSound; // Add this line for the pause sound
    public GameObject inGameMenu;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (GameIsPaused)
            {
                Resume();
                EventSystem eventSystem = GetComponentInChildren<EventSystem>();
                if (eventSystem != null)
                {
                    eventSystem.enabled = false;
                }
            }
            else
            {
                Pause();
                EventSystem eventSystem = GetComponentInChildren<EventSystem>();
                if (eventSystem != null)
                {
                    eventSystem.enabled = true;
                }
            }
        }
    }

    void Pause()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;

        // Play pause sound when pausing

        AudioSource.PlayClipAtPoint(pauseSound, Camera.main.transform.position);


        // Show your pause menu UI (activate your canvas)
        inGameMenu.SetActive(false);
        pauseMenuUI.SetActive(true);
        // Disable scripts that should not run during pause (e.g., player movement scripts)
        DisablePlayerScripts();
    }

    void Resume()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        // Hide your pause menu UI (deactivate your canvas)
        inGameMenu.SetActive(true);
        pauseMenuUI.SetActive(false);
        // Enable scripts that were disabled during pause
        EnablePlayerScripts();
    }

    void DisablePlayerScripts()
    {

        // Disable scripts that should not run during pause
        MouseLook mouseLook = GetComponentInChildren<MouseLook>();
        if (mouseLook != null)
        {
            mouseLook.enabled = false;
        }

        FPSInputController fpsInputController = GetComponentInChildren<FPSInputController>();
        if (fpsInputController != null)
        {
            fpsInputController.enabled = false;
        }
        // Additional disabling logic...
        
    }

    void EnablePlayerScripts()
    {
        MouseLook mouseLook = GetComponentInChildren<MouseLook>();
        if (mouseLook != null)
        {
            mouseLook.enabled = true;
        }

        FPSInputController fpsInputController = GetComponentInChildren<FPSInputController>();
        if (fpsInputController != null)
        {
            fpsInputController.enabled = true;
        }
    }

    public bool getGameIsPaused()
    {
        return GameIsPaused;
    }

    void EnableMouseControlForUI()
    {
        // Unlock the cursor and make it visible for UI interactions
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void DisableMouseControlForUI()
    {
        // Lock the cursor and make it invisible for regular gameplay
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}