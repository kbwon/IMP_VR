using UnityEngine;

public class CameraSound : MonoBehaviour
{
    [SerializeField]
    private AudioClip cameraSound; // Audio clip to be played when object is active

    private AudioSource audioSource; // Reference to AudioSource component

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Get AudioSource component at start
    }

    void Update()
    {
        if (gameObject.activeSelf) // Check if the GameObject is currently active
        {
            audioSource.clip = cameraSound; // Set audio clip
            audioSource.Play(); // Play the audio
        }
    }
}
