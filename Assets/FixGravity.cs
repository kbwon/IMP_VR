using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.XR.Interaction.Toolkit.Locomotion;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Climbing;

// fixes gravity application problem when climbing and using Continuous Move.
public class FixGravity : MonoBehaviour
{
    private CharacterController characterController;
    private ClimbProvider climbProvider;

    [SerializeField]
    private bool forceGravityCheck = false;

    void Awake()
    {
        characterController = FindAnyObjectByType<CharacterController>();
        climbProvider = FindAnyObjectByType<ClimbProvider>();
    }

    private void OnEnable()
    {
        climbProvider.locomotionStarted += LocomotionStarted;
        climbProvider.locomotionEnded += LocomotionEnded;
    }

    private void OnDisable()
    {
        climbProvider.locomotionStarted -= LocomotionStarted;
        climbProvider.locomotionEnded -= LocomotionEnded;
    }

    void Update()
    {
        if (forceGravityCheck)
        {
            characterController.SimpleMove(Vector3.zero);
        }
    }

    private void LocomotionStarted(LocomotionProvider providers)
    {
        forceGravityCheck = false;
    }

    private void LocomotionEnded(LocomotionProvider providers)
    {
        forceGravityCheck = true;
    }
}
