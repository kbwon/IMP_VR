using UnityEngine;

public class BookheadMover : MonoBehaviour
{
    [Header("Object to Move")]
    public GameObject targetObject; // Object to be moved

    [Header("Movement Path")]
    public Transform startPoint;  // Start position
    public Transform endPoint;    // End position

    public AudioClip bookHeadAppear; // Sound to play when appearing

    public float moveSpeed = 2f;

    private bool isMoving = false;
    private Transform targetTransform;
    private AudioSource audioSource;

    void Start()
    {
        // If target is assigned, deactivate it
        if (targetObject != null)
        {
            targetTransform = targetObject.transform;
            targetObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("⚠️ targetObject is not assigned!");
        }

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // ✅ Detect spacebar input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ActivateAndMove();
        }

        if (isMoving && targetTransform != null && endPoint != null)
        {
            // Handle movement
            targetTransform.position = Vector3.MoveTowards(targetTransform.position, endPoint.position, moveSpeed * Time.deltaTime);

            // Once reached destination, deactivate
            if (Vector3.Distance(targetTransform.position, endPoint.position) < 0.01f)
            {
                isMoving = false;
                targetObject.SetActive(false); // Deactivate after arrival
            }
        }
    }

    // Called externally: set position to start and begin movement
    public void ActivateAndMove()
    {
        if (targetObject != null && startPoint != null)
        {
            targetTransform.position = startPoint.position;
            targetObject.SetActive(true); // Activate
            isMoving = true;
            audioSource.clip = bookHeadAppear;
            audioSource.Play();
        }
    }

    // Called externally to forcibly deactivate
    public void DeactivateImmediately()
    {
        if (targetObject != null)
        {
            isMoving = false;
            targetObject.SetActive(false);
        }
    }
}
