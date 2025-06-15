using UnityEngine;

public class CameraSound : MonoBehaviour
{
    [SerializeField]
    private AudioClip cameraSound;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();  
    }
    void Update()
    {
        if (gameObject.activeSelf)
        {
            audioSource.clip = cameraSound;
            audioSource.Play();
        }
    }
}
