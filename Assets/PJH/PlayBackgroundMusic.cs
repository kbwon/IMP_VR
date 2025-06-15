using UnityEngine;

// Plays a given AudioClip as looping background music through XR Origin's AudioSource
public class BackgroundMusicPlayer : MonoBehaviour
{
    public AudioClip musicClip; // Assign the music clip in Inspector

    private AudioSource audioSource;

    void Awake()
    {
        // Find or get the AudioSource on XR Origin
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            print("No AudioSource");
        }
    }

    void Start()
    {
        if (musicClip != null)
        {
            audioSource.clip = musicClip;
            audioSource.loop = true; // Loop the music
            audioSource.Play();
        }
    }
}
