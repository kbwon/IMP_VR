using System.Collections;
using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class CameraController : MonoBehaviour
{
    [SerializeField] private InputActionProperty triggerAction; // Input action for triggering the camera
    [SerializeField] private GameObject myTunnelingVignette;    // Custom tunneling vignette object
    [SerializeField] private GameObject tunnelingVignette;      // Default tunneling vignette object
    [SerializeField] private GameObject hud;                    // HUD GameObject
    [SerializeField] private Text activeTimeText1;              // First UI text to display active time
    [SerializeField] private Text activeTimeText2;              // Second UI text to display active time
    [SerializeField] private AudioClip cameraSound;             // Audio clip played when camera is activated

    private AudioSource audioSource;        // AudioSource component reference
    private float activeTime = 100f;        // Total active time for the camera
    private float coolDown = 0f;            // Unused cooldown variable (reserved for future use)
    private bool isCameraActive = false;    // Flag to check if camera mode is active
    private bool wasPressed = false;        // To detect edge trigger of button press

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Get AudioSource component on start
    }

    private void Update()
    {
        bool isPressed = triggerAction.action.IsPressed(); // Check if the trigger is currently pressed

        if (isPressed && !wasPressed) // Detect trigger press (only on rising edge)
        {
            isCameraActive = !isCameraActive; // Toggle camera mode

            if (isCameraActive && activeTime > 0)
            {
                Debug.Log("Camera On");
                audioSource.clip = cameraSound;
                audioSource.Play();
                CameraManager.Instance.EnterCameraMode(); // Call method to enter camera mode
                if (tunnelingVignette != null) tunnelingVignette.SetActive(false); // Disable default vignette
                if (myTunnelingVignette != null) myTunnelingVignette.SetActive(true); // Enable custom vignette
                if (hud != null) hud.SetActive(true); // Show HUD
            }
            else
            {
                Debug.Log("Camera Off");
                CameraManager.Instance.ExitCameraMode(); // Call method to exit camera mode
                if (tunnelingVignette != null) tunnelingVignette.SetActive(false);
                if (myTunnelingVignette != null) myTunnelingVignette.SetActive(true);
                if (tunnelingVignette != null) tunnelingVignette.SetActive(true);
                if (myTunnelingVignette != null) myTunnelingVignette.SetActive(false);
                if (hud != null) hud.SetActive(false); // Hide HUD
            }
        }

        // Decrease active time while camera mode is active
        if (isCameraActive)
        {
            activeTime -= Time.deltaTime;
            if (activeTime <= 0)
            {
                Debug.Log("Camera Shut Down");
                isCameraActive = false;
                CameraManager.Instance.ExitCameraMode(); // Automatically exit camera mode when time runs out
                if (tunnelingVignette != null) tunnelingVignette.SetActive(false);
                if (myTunnelingVignette != null) myTunnelingVignette.SetActive(true);
                if (hud != null) hud.SetActive(false);
                activeTime = 0;
            }
        }

        // Update UI text with remaining active time
        if (activeTimeText1 != null)
        {
            activeTimeText1.text = Mathf.Ceil(activeTime).ToString() + "%";
        }
        if (activeTimeText2 != null)
        {
            activeTimeText2.text = Mathf.Ceil(activeTime).ToString() + "%";
        }

        wasPressed = isPressed; // Save current input state for edge detection
    }
}
