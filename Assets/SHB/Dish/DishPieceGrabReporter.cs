using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class DishPieceGrabReporter : MonoBehaviour
{
    private XRGrabInteractable grab; // Reference to the XR grab interaction component
    private GetBreakingDish parentScript; // Reference to the parent script managing dish pieces

    // Called when the object is initialized (before Start)
    void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
        parentScript = GetComponentInParent<GetBreakingDish>();
    }

    // Registers the grab listener when the object is enabled
    void OnEnable()
    {
        grab.selectEntered.AddListener(OnGrabbed);
    }

    // Unregisters the grab listener when the object is disabled
    void OnDisable()
    {
        grab.selectEntered.RemoveListener(OnGrabbed);
    }

    // Called when the dish piece is grabbed by the player
    private void OnGrabbed(SelectEnterEventArgs args)
    {
        if (parentScript != null)
        {
            parentScript.getBreakingDish(this.gameObject); // Pass this dish piece to the parent
        }
    }
}
