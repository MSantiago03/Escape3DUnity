using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simon : MonoBehaviour
{
    // List to store the sequence of buttons to be clicked
    private List<int> buttonsToClick = new List<int>();

    // List to store the buttons clicked by the player
    private List<int> buttonsClicked = new List<int>();

    // Audio source for playing sounds
    private AudioSource audioSource;

    // Sound clips for different game events
    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioClip successSound;
    public AudioClip startSound;
    public AudioClip clickSound;

    // Prefab for text box
    public GameObject textBoxPrefab;

    // Reference to the start button
    public GameObject startButton;

    // Flag to prevent starting the game multiple times in quick succession
    private bool startDisable = false;

    // List of buttons in the Simon game
    public List<GameObject> buttons;

    // Reference to the player's camera
    [SerializeField]
    private Transform playerCameraTransform;

    // Distance for raycasting to detect selectable objects
    private float pickUpDistance = 4f;

    // Flag to control button interactivity
    private bool buttonsInteractable = false;

    // Number of buttons to win the game
    private int numberToWin = 8;

    // Flags indicating game status
    private bool gameOver;
    private bool gameWon = false;

    // Reference to the letter object
    public GameObject letter;

    // Called when the script is first run
    void Start()
    {
        // Initialize the letter object as inactive
        letter.SetActive(false);

        // Add an AudioSource component to the same GameObject as this script
        audioSource = gameObject.AddComponent<AudioSource>();

        // Invoke the GenerateTextBox method every 900 seconds (15 minutes)
        InvokeRepeating("GenerateTextBox", 900f, 900f);
    }

    // Method to handle player click on a button
    public void CreatePlayerList(int buttonID)
    {
        // Add the clicked button to the list
        buttonsClicked.Add(buttonID);

        // Check for a mismatch in the sequence
        for (int i = 0; i < buttonsClicked.Count; i++)
        {
            if (buttonsToClick[i] != buttonsClicked[i])
            {
                StartCoroutine(PlayerLost());
                return;
            }
        }
        audioSource.PlayOneShot(winSound);

        // Check if the player completed the sequence
        if (buttonsClicked.Count == buttonsToClick.Count)
        {
            if (buttonsClicked.Count == numberToWin)
            {
                GameWon();
                return;
            }

            StartCoroutine(StartNextRound());
        }
        StartCoroutine(ButtonHighlight(buttonID));
    }

    // Method to start the game
    public void StartGame()
    {
        audioSource.PlayOneShot(startSound);
        gameOver = false;
        buttonsToClick.Clear();
        buttonsClicked.Clear();
        StartCoroutine(EnableStartButtonAfterDelay(5f));
        StartCoroutine(StartNextRound());
    }

    // Coroutine to enable the start button after a delay
    private IEnumerator EnableStartButtonAfterDelay(float delay)
    {
        startDisable = true;
        yield return new WaitForSeconds(delay);
        startDisable = false;
    }

    // Method to list button coroutines
    private void ListButtonCoroutines()
    {
        List<Coroutine> highlightCoroutines = new List<Coroutine>();
        foreach (GameObject button in buttons)
        {
            if (button.CompareTag("SimonButton"))
            {
                Coroutine coroutine = StartCoroutine(ButtonHighlightAll(button));
                highlightCoroutines.Add(coroutine);
            }
        }
    }

    // Method called when the game is won
    public void GameWon()
    {
        gameWon = true;
        buttonsInteractable = false;
        audioSource.PlayOneShot(successSound);
        letter.SetActive(true);
        ListButtonCoroutines();
        textBoxPrefab.SetActive(false);
    }

    // Coroutine to start the next round
    public IEnumerator StartNextRound()
    {
        buttonsInteractable = false;
        buttonsClicked.Clear();
        yield return new WaitForSeconds(1f);

        // Add a random button to the sequence
        buttonsToClick.Add(Random.Range(0, buttons.Count));

        // Play the sequence of buttons
        foreach (int index in buttonsToClick)
        {
            audioSource.PlayOneShot(clickSound);
            yield return StartCoroutine(ButtonHighlight(index));
            yield return new WaitForSeconds(0.5f);
        }

        buttonsInteractable = true;
        yield return null;
    }

    // Coroutine to handle player loss
    public IEnumerator PlayerLost()
    {
        gameOver = true;
        buttonsInteractable = false;
        audioSource.PlayOneShot(loseSound);

        // Highlight all buttons for visual feedback
        ListButtonCoroutines();

        // Clear lists and activate the start button
        buttonsClicked.Clear();
        buttonsToClick.Clear();
        yield return new WaitForSeconds(2f);
        startButton.SetActive(true);
    }

    // Coroutine to highlight all buttons
    public IEnumerator ButtonHighlightAll(GameObject button)
    {
        // Get the button renderer
        Renderer renderer = button.GetComponent<Renderer>();

        // Store the original color
        Color originalColor = renderer.material.color;

        // Set the button color to red or green based on game outcome
        if (gameOver)
        {
            renderer.material.color = Color.red;
        }
        else
        {
            renderer.material.color = Color.green;
        }

        // Wait for a short duration
        yield return new WaitForSeconds(2f);

        // Set the button color back to the original color
        renderer.material.color = originalColor;
    }

    // Coroutine to highlight a specific button
    public IEnumerator ButtonHighlight(int buttonID)
    {
        // Get the renderer of the button
        Renderer renderer = buttons[buttonID].GetComponent<Renderer>();

        // Store the original color
        Color originalColor = renderer.material.color;

        // Darken the color
        Color darkerColor = originalColor * 0.5f;

        // Set the button color to the darker color
        renderer.material.color = darkerColor;

        // Wait for a short duration
        if (buttonsInteractable)
        {
            yield return new WaitForSeconds(0.1f);
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
        }

        // Set the button color back to the original color abruptly
        renderer.material.color = originalColor;
    }

    // Update is called once per frame
    void Update()
    {
        // Check for the 'E' key to simulate a button click
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryClickButton();
        }
    }

    // Method to handle player input when clicking buttons
    private void TryClickButton()
    {
        RaycastHit hit;

        // Check if the raycast hits an object within the pickUpDistance
        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out hit, pickUpDistance))
        {
            // Check if the hit object is the start button
            if (hit.collider.gameObject == startButton && !startDisable)
            {
                // If the hit object is the start button, start the game
                StartGame();
            }
            // Check if the hit object is a Simon button and the buttons are interactable
            if (hit.collider.CompareTag("SimonButton") && buttonsInteractable)
            {
                // If the hit object is one of the buttons, simulate a click
                GameObject hitObject = hit.collider.gameObject;
                int buttonIndex = buttons.IndexOf(hitObject);

                if (buttonIndex != -1)
                {
                    CreatePlayerList(buttonIndex);
                }
            }
        }
    }

    // Method to generate a text box
    private void GenerateTextBox()
    {
        // Display the text box if the game is not won
        if (!gameWon)
        {
            textBoxPrefab.SetActive(true);
        }
    }
}
