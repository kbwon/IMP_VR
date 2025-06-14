using UnityEngine;
using UnityEngine.Audio;

// Plays and stops walking sound instantly as player moves or stops
public class WalkingSound : MonoBehaviour
{
    public AudioClip walkingClip;
    public float moveThreshold = 0.1f;

    private Vector3 lastPosition;
    private AudioSource walkingSource;
    private bool isMoving = false;
    private float checkInterval = 0.1f; // How often to check movement (seconds)
    private float checkTimer = 0f;
    public AudioMixerGroup mixer;

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        checkTimer += Time.deltaTime;
        if (checkTimer < checkInterval) return;
        checkTimer = 0f;

        float distance = Vector3.Distance(transform.position, lastPosition);
        bool nowMoving = distance > moveThreshold;

        if (nowMoving && !isMoving)
        {
            // Start walking sound
            walkingSource = gameObject.AddComponent<AudioSource>();
            walkingSource.clip = walkingClip;
            walkingSource.loop = true;
            walkingSource.outputAudioMixerGroup = mixer;
            walkingSource.Play();
            isMoving = true;
        }
        else if (!nowMoving && isMoving)
        {
            // Stop walking sound instantly
            if (walkingSource != null)
            {
                walkingSource.Stop();
                Destroy(walkingSource);
            }
            isMoving = false;
        }

        // LastPosition is only updated at regular intervals
        lastPosition = transform.position;
    }
}
