using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

// Toggles the child Canvas when the Note is grabbed.
public class FindNote : MonoBehaviour
{
    private Canvas noteCanvas; // The Canvas to show/hide
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable; // Grab component
    private bool isCanvasActive = false; // Canvas state

    public Transform playerCamera; // Player camera (not used here)

    void Awake()
    {
        // Find the child Canvas
        noteCanvas = GetComponentInChildren<Canvas>(true);
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
    }

    void OnEnable()
    {
        // Listen for grab event
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnSelectEntered);
        }
    }

    void OnDisable()
    {
        // Stop listening for grab event
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnSelectEntered);
        }
    }

    // Toggle Canvas on grab
    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        isCanvasActive = !isCanvasActive;
        if (noteCanvas != null)
        {
            noteCanvas.gameObject.SetActive(isCanvasActive);
        }
    }
}
