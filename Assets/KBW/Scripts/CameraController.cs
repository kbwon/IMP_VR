using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private InputActionProperty triggerAction;
    private bool isViewfinderActive = false;

    private void Update()
    {
        if (triggerAction.action.WasPressedThisFrame())
        {
            Debug.Log("Camera on");
            isViewfinderActive = true;
            CameraManager.Instance.EnterViewMode();
        }

        if (triggerAction.action.WasReleasedThisFrame())
        {
            Debug.Log("Camera off");
            isViewfinderActive = false;
            CameraManager.Instance.ExitViewMode();
        }
    }
}
