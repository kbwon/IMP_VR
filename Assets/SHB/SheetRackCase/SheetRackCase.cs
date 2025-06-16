using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SheetRackCase : MonoBehaviour
{
    public bool isOpen = false; // Current open/closed state of the drawer
    public TextMeshPro guideletter; // UI text for interaction instructions
    public XRGrabInteractable xrGrab; // Reference to grab interaction component
    public Sounds sound; // Sound manager for playing open/close sounds
    private bool isMoving = false; // Whether the drawer is currently moving

    private Vector3 closedPosition; // Local position when closed
    private Vector3 openPosition; // Local position when open
    private Coroutine moveCoroutine; // Reference to running movement coroutine

    // Sets initial drawer position based on isOpen state
    void Start()
    {
        closedPosition = transform.localPosition;
        openPosition = closedPosition + new Vector3(0f, 0f, -0.428f);

        if (isOpen == true) transform.localPosition += new Vector3(0f, 0f, -0.428f);
    }

    // Called when the player presses the drawer handle
    public void pressSheetRack()
    {
        if (isMoving)
        {
            return;
        }

        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);

        Vector3 target = isOpen ? closedPosition : openPosition;
        moveCoroutine = StartCoroutine(MoveToPosition(target));

        isOpen = !isOpen;

        if (isOpen == false) guideletter.text = "Press Grab to open";
        else guideletter.text = "Press Grab to close";

        sound.PlayRandomSound();
    }

    // Moves the drawer smoothly to the target position over time
    IEnumerator MoveToPosition(Vector3 target)
    {
        isMoving = true;
        xrGrab.enabled = false;

        float duration = 0.8f;
        float elapsed = 0f;
        Vector3 start = transform.localPosition;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            transform.localPosition = Vector3.Lerp(start, target, t);
            yield return null;
        }

        transform.localPosition = target;
        isMoving = false;
        xrGrab.enabled = true;
    }
}
