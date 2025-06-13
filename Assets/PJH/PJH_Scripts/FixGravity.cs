using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.XR.Interaction.Toolkit.Locomotion;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Climbing;

// Fixes gravity issues when using climbing and continuous movement together.
public class FixGravity : MonoBehaviour
{
    private CharacterController characterController; // Reference to the CharacterController
    private ClimbProvider climbProvider;             // Reference to the ClimbProvider

    [SerializeField]
    private bool forceGravityCheck = false; // Controls if gravity should be forced

    void Awake()
    {
        // Find required components in the scene
        characterController = FindAnyObjectByType<CharacterController>();
        climbProvider = FindAnyObjectByType<ClimbProvider>();
    }

    private void OnEnable()
    {
        // Subscribe to climbing events
        climbProvider.locomotionStarted += LocomotionStarted;
        climbProvider.locomotionEnded += LocomotionEnded;
    }

    private void OnDisable()
    {
        // Unsubscribe from climbing events
        climbProvider.locomotionStarted -= LocomotionStarted;
        climbProvider.locomotionEnded -= LocomotionEnded;
    }

    void Update()
    {
        // Force gravity by calling SimpleMove when needed
        if (forceGravityCheck)
        {
            characterController.SimpleMove(Vector3.zero);
        }
    }

    // Called when climbing starts
    private void LocomotionStarted(LocomotionProvider providers)
    {
        forceGravityCheck = false;
    }

    // Called when climbing ends
    private void LocomotionEnded(LocomotionProvider providers)
    {
        forceGravityCheck = true;
    }
}
