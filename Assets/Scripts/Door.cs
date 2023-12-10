using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool IsOpen = false;
    [SerializeField] private float Speed = 1f;
    [Header("Rotation Configs")] [SerializeField] private float RotationAmount = 90f;
    [Header("Audio")] [SerializeField] private AudioClip doorOpenSound;

    private Vector3 StartRotation;
    private Coroutine AnimationCoroutine;
    private AudioSource audioSource;

    private void Awake()
    {
        StartRotation = transform.rotation.eulerAngles;
        audioSource = GetComponent<AudioSource>();
        // Add an AudioSource component to the same GameObject as this script
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void Open()
    {
        if (!IsOpen)
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }

            AnimationCoroutine = StartCoroutine(DoRotationOpen(RotationAmount));

            // Play door opening sound
            if (doorOpenSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(doorOpenSound);
            }
        }
    }

    private IEnumerator DoRotationOpen(float rotationInput)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(new Vector3(StartRotation.x, StartRotation.y + rotationInput, StartRotation.z));

        IsOpen = true;

        float time = 0;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }
    }
}

