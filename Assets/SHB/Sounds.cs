using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioSource audioSource;             // AudioSource component for playback
    public List<AudioClip> sounds = new();      // List of possible sound clips

    public void PlayRandomSound()
    {
        if (sounds.Count == 0) return;

        AudioClip clip = sounds[Random.Range(0, sounds.Count)];
        audioSource.PlayOneShot(clip); // Plays sound even if another is already playing
    }
}
