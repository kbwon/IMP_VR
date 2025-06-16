using JetBrains.Annotations;
using UnityEngine;

public class Statue : MonoBehaviour
{
    public bool isStared; // Whether the player is staring at the statue

    [SerializeField]
    private float stareDistance = 5f; // Distance within which staring detection is active
    [SerializeField]
    private float maxIgnoreTime = 5f; // Max time before player dies if not staring
    [SerializeField]
    private float viewAngle = 75f; // Field of view angle for staring detection
    [SerializeField]
    private AudioClip stareSound; // Sound played when not stared at
    [SerializeField]
    private GameObject redLight; // Red light indicator when being stared at

    private float ignoreTimer = 0f; // Timer for tracking ignoring duration
    private GameObject eyes; // Reference to statue's eyes object
    private Transform cameraPos; // Player's camera position
    private AudioSource audioSource; // AudioSource component reference

    void Start()
    {
        eyes = transform.GetChild(0).gameObject; // Get first child as eyes
        audioSource = GetComponent<AudioSource>(); // Get AudioSource component
    }

    void Update()
    {
        cameraPos = Camera.main.transform; // Get main camera transform

        Vector3 statueDir = transform.position - cameraPos.position; // Vector from camera to statue
        float angle = Vector3.Angle(Camera.main.transform.forward, statueDir); // Angle between camera forward and statue direction

        float playerDistance = Vector3.Distance(transform.position, cameraPos.position); // Distance between player and statue
        bool isInRange = playerDistance <= stareDistance; // Check if within stare range

        if (isInRange)
        {
            if (angle <= viewAngle) // Player is staring at the statue
            {
                isStared = true;
                Vector3 lookDir = cameraPos.position - transform.position;
                Quaternion baseRotation = Quaternion.LookRotation(-lookDir);
                Quaternion yOffset = Quaternion.Euler(0, 60f, 0);
                transform.rotation = baseRotation * yOffset; // Rotate statue to face player

                if (eyes != null) eyes.SetActive(true); // Show eyes
                if (redLight != null) redLight.SetActive(true); // Turn on red light

                ignoreTimer = 0f; // Reset ignore timer

                audioSource.Stop(); // Stop warning sound
            }
            else // Player is not staring directly
            {
                isStared = false;

                if (eyes != null) eyes.SetActive(false); // Hide eyes
                if (redLight != null) redLight.SetActive(false); // Turn off red light

                if (!audioSource.isPlaying)
                {
                    audioSource.clip = stareSound;
                    audioSource.Play(); // Play warning sound
                }

                ignoreTimer += Time.deltaTime; // Increase ignore timer
                if (ignoreTimer >= maxIgnoreTime)
                {
                    Debug.Log("You died");
                    PlayerInfo.Instance.isDead = true; // Mark player as dead
                    PlayerInfo.Instance.whenPlayerDied(); // Handle player death
                    ignoreTimer = 0f;
                }
            }
        }
        else // Player is out of range
        {
            isStared = false;
            if (eyes != null) eyes.SetActive(false);
            if (redLight != null) redLight.SetActive(false);
            audioSource.Stop();
        }
    }

    // Method to teleport the statue to a new position
    public void StatueTeleport(Vector3 pos)
    {
        transform.position = pos;
    }
}
