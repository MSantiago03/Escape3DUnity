using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseGame : MonoBehaviour
{
    // Flag to track whether the game is paused
    public bool GameIsPaused = false;

    // UI for the pause menu
    public GameObject pauseMenuUI;

    // Sound played when the game is paused
    public AudioClip pauseSound;

    // In-game menu UI
    public GameObject inGameMenu;

    // Called at the start of the script's execution
    void Start()
    {
        // Initialization logic, if any
    }

    // Called every frame
    void Update()
    {
        // Check for the "P" key to toggle pause
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (GameIsPaused)
            {
                // If the game is already paused, resume it
                Resume();
                // Disable the EventSystem for UI interactions
                DisableEventSystem();
            }
            else
            {
                // If the game is not paused, pause it
                Pause();
                // Enable the EventSystem for UI interactions
                EnableEventSystem();
            }
        }
    }

    // Method to pause the game
    void Pause()
    {
        // Set time scale to 0 to freeze the game
        Time.timeScale = 0f;
        GameIsPaused = true;

        // Play pause sound when pausing
        AudioSource.PlayClipAtPoint(pauseSound, Camera.main.transform.position);

        // Deactivate in-game menu UI and activate pause menu UI
        inGameMenu.SetActive(false);
        pauseMenuUI.SetActive(true);

        // Disable scripts that should not run during pause (e.g., player movement scripts)
        DisablePlayerScripts();

        // Enable mouse control for UI interactions
        EnableMouseControlForUI();
    }

    // Method to resume the game
    void Resume()
    {
        // Set time scale back to 1 to resume the game
        Time.timeScale = 1f;
        GameIsPaused = false;

        // Activate in-game menu UI and deactivate pause menu UI
        inGameMenu.SetActive(true);
        pauseMenuUI.SetActive(false);

        // Enable scripts that were disabled during pause
        EnablePlayerScripts();

        // Disable mouse control for regular gameplay
        DisableMouseControlForUI();
    }

    // Method to disable player-related scripts during pause
    void DisablePlayerScripts()
    {
        // Disable mouse look script
        MouseLook mouseLook = GetComponentInChildren<MouseLook>();
        if (mouseLook != null)
        {
            mouseLook.enabled = false;
        }

        // Disable FPSInputController script
        FPSInputController fpsInputController = GetComponentInChildren<FPSInputController>();
        if (fpsInputController != null)
        {
            fpsInputController.enabled = false;
        }

        // Additional disabling logic for other scripts...
    }

    // Method to enable player-related scripts after pause
    void EnablePlayerScripts()
    {
        // Enable mouse look script
        MouseLook mouseLook = GetComponentInChildren<MouseLook>();
        if (mouseLook != null)
        {
            mouseLook.enabled = true;
        }

        // Enable FPSInputController script
        FPSInputController fpsInputController = GetComponentInChildren<FPSInputController>();
        if (fpsInputController != null)
        {
            fpsInputController.enabled = true;
        }

        // Additional enabling logic for other scripts...
    }

    // Method to get the current pause state
    public bool GetGameIsPaused()
    {
        return GameIsPaused;
    }

    // Method to enable mouse control for UI interactions
    void EnableMouseControlForUI()
    {
        // Unlock the cursor and make it visible for UI interactions
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Method to disable mouse control for regular gameplay
    void DisableMouseControlForUI()
    {
        // Lock the cursor and make it invisible for regular gameplay
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Method to disable the EventSystem for UI interactions
    void DisableEventSystem()
    {
        EventSystem eventSystem = GetComponentInChildren<EventSystem>();
        if (eventSystem != null)
        {
            eventSystem.enabled = false;
        }
    }

    // Method to enable the EventSystem for UI interactions
    void EnableEventSystem()
    {
        EventSystem eventSystem = GetComponentInChildren<EventSystem>();
        if (eventSystem != null)
        {
            eventSystem.enabled = true;
        }
    }
}
