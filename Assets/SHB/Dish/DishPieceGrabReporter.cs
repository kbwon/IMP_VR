using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class DishPieceGrabReporter : MonoBehaviour
{
    private XRGrabInteractable grab;
    private GetBreakingDish parentScript;

    void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
        parentScript = GetComponentInParent<GetBreakingDish>();
    }

    void OnEnable()
    {
        grab.selectEntered.AddListener(OnGrabbed);
    }

    void OnDisable()
    {
        grab.selectEntered.RemoveListener(OnGrabbed);
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        if (parentScript != null)
        {
            parentScript.getBreakingDish(gameObject);  // 자기 자신 넘겨줌
        }
    }
}
