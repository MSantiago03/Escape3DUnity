using System.Collections;
using UnityEngine;

public class FPSInputController : MonoBehaviour
{
    private CharacterMotor motor;
    private AudioSource audioSource;
    public AudioClip walkingSound;
    public AudioClip jumpingSound;

    private Coroutine walkingSoundCoroutine;
    private Collider myCollider;

    // Use this for initialization
    void Awake()
    {
        motor = GetComponent<CharacterMotor>();
        audioSource = GetComponent<AudioSource>();
        myCollider = gameObject.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get the input vector from the keyboard or analog stick
        Vector3 directionVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        // Check for jumping before updating motor.inputJump
        bool jumping = Input.GetButton("Jump");

        if (directionVector != Vector3.zero)
        {
            // Play walking sound when moving
            if (!audioSource.isPlaying && walkingSound != null)
            {
                audioSource.clip = walkingSound;
                audioSource.Play();

                if (jumping)
                {
                    walkingSoundCoroutine = StartCoroutine(PauseWalkingSound());
                }
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }

        // Apply the direction to the CharacterMotor
        motor.inputMoveDirection = transform.rotation * directionVector;
        motor.inputJump = jumping;

        // Play jumping sound when jumping
        if (jumping && jumpingSound != null && !audioSource.isPlaying)
        {
            walkingSoundCoroutine = StartCoroutine(PauseWalkingSound());
            audioSource.clip = jumpingSound;
            audioSource.Play();
        }
    }

    IEnumerator PauseWalkingSound()
    {
        audioSource.clip = walkingSound;
        audioSource.Pause();
        // Wait for a short duration (adjust as needed)
        yield return new WaitForSeconds(2f);
    }
}
