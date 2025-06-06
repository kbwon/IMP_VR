using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> sounds = new();

    public void PlayRandomSound()
    {
        if (sounds.Count == 0) return;

        AudioClip clip = sounds[Random.Range(0, sounds.Count)];
        audioSource.PlayOneShot(clip); // 기존 소리와 겹쳐도 재생 가능
    }
}
