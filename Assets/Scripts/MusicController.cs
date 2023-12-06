using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{

    public AudioClip musicClip;  // Drag and drop your music clip to this field in the Unity Editor
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = musicClip;
        audioSource.Play();

    }

    // Update is called once per frame
    public void stopMusic()
    {
        audioSource.Stop();
    }
}
