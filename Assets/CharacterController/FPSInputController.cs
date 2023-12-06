using UnityEngine;
using System.Collections;

// Require a character controller to be attached to the same game object
[RequireComponent (typeof(CharacterMotor))]
[AddComponentMenu ("Character/FPS Input Controller")]

public class FPSInputController : MonoBehaviour
{
    private CharacterMotor motor;
    private AudioSource audioSource;
    public AudioClip walkingSound;
    public AudioClip jumpingSound;

    // Use this for initialization
    void Awake()
    {
        motor = GetComponent<CharacterMotor>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get the input vector from keyboard or analog stick
        Vector3 directionVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (directionVector != Vector3.zero)
        {
            // ... (rest of your existing code)

            // Play walking sound when moving
            if (!audioSource.isPlaying && walkingSound != null)
            {
                audioSource.clip = walkingSound;
                audioSource.Play();
            }
        }
        else
        {
            // Stop walking sound when not moving
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }

        // Apply the direction to the CharacterMotor
        motor.inputMoveDirection = transform.rotation * directionVector;
        motor.inputJump = Input.GetButton("Jump");

        // Play jumping sound when jumping
        if (motor.inputJump && jumpingSound != null && !audioSource.isPlaying)
        {
            audioSource.clip = jumpingSound;
            audioSource.Play();
        }
    }
}

