using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simon : MonoBehaviour
{
    private List<int> buttonsToClick = new List<int>();
    private List<int> buttonsClicked = new List<int>();

    private AudioSource audioSource;

    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioClip successSound;
    public AudioClip startSound;
    public AudioClip clickSound;


    public GameObject startButton;

    private bool startDisable = false;

    public List<GameObject> buttons; // Change to GameObject list

    [SerializeField]
    private Transform playerCameraTransform;
    [SerializeField]
    private Transform objectGrabPointTransform;
    [SerializeField]
    private LayerMask pickUpLayerMask;
    [SerializeField]
    private float pickUpDistance = 4f;

    private bool buttonsInteractable = false;

    private int numberToWin = 7;

    private bool gameOver;

    private bool gameWon = false;


    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void CreatePlayerList(int buttonID)
    {
        buttonsClicked.Add(buttonID);
        Debug.Log("buttonsClicked count " + buttonsClicked.Count);

        // Check for a mismatch in the sequence
        for (int i = 0; i < buttonsClicked.Count; i++)
        {
            if (buttonsToClick[i] != buttonsClicked[i])
            {
                Debug.Log("Mismatch at index " + i);
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

    public void StartGame()
    {
        audioSource.PlayOneShot(startSound);
        gameOver = false;
        buttonsToClick.Clear();
        buttonsClicked.Clear();
        StartCoroutine(EnableStartButtonAfterDelay(5f));
        StartCoroutine(StartNextRound());
    }

private IEnumerator EnableStartButtonAfterDelay(float delay)
{
    startDisable = true;
    yield return new WaitForSeconds(delay);
    startDisable = false;
}

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

    public void GameWon()
    {
        gameWon = true;
        buttonsInteractable = false;
        audioSource.PlayOneShot(successSound);
        ListButtonCoroutines();
    }

    public IEnumerator StartNextRound()
    {
        buttonsInteractable = false;
        buttonsClicked.Clear();
        Debug.Log("nextRound");
        yield return new WaitForSeconds(1f);
        buttonsToClick.Add(Random.Range(0, buttons.Count));
        Debug.Log("StartNext buttonsToClick[i]" + buttonsToClick[0]);
        foreach (int index in buttonsToClick)
        {
            audioSource.PlayOneShot(clickSound);
            yield return StartCoroutine(ButtonHighlight(index));
            yield return new WaitForSeconds(0.5f);
        }
        buttonsInteractable = true;
        yield return null;
    }

    public IEnumerator PlayerLost()
    {
        gameOver = true;
        buttonsInteractable = false;
        audioSource.PlayOneShot(loseSound);

        ListButtonCoroutines();

        // Clear lists and activate the start button
        buttonsClicked.Clear();
        buttonsToClick.Clear();
        yield return new WaitForSeconds(2f);
        startButton.SetActive(true);
    }

    public IEnumerator ButtonHighlightAll(GameObject button)
    {
        
        Debug.Log("highlight in red");

        // Get the button renderer
        Renderer renderer = button.GetComponent<Renderer>();

        // Store the original color
        Color originalColor = renderer.material.color;

        // Set the button color to red
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

    public IEnumerator ButtonHighlight(int buttonID)
    {
        Debug.Log("highlight");

        Renderer renderer = buttons[buttonID].GetComponent<Renderer>();
        Color originalColor = renderer.material.color;

        // Darken the color
        Color darkerColor = originalColor * 0.5f;

        // Set the button color to the darker color
        renderer.material.color = darkerColor;

        // Wait for half a second
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryClickButton();
        }
    }

    private void TryClickButton()
    {
        RaycastHit hit;

        // Check if the raycast hits an object within the pickUpDistance
        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out hit, pickUpDistance))
        {
            // Check if the hit object is the start button
            if (hit.collider.gameObject == startButton & startDisable == false)
            {
                // If the hit object is the start button, start the game
                StartGame();
            }
            if (hit.collider.CompareTag("SimonButton") && buttonsInteractable)
            {
                
                Debug.Log("Clicked on another button");
                // If the hit object is one of the buttons, simulate a click
                GameObject hitObject = hit.collider.gameObject;
                int buttonIndex = buttons.IndexOf(hitObject);


                Debug.Log("index is" + buttonIndex);

                if (buttonIndex != -1)
                {
                    CreatePlayerList(buttonIndex);
                }
            }
        }
    }
}
