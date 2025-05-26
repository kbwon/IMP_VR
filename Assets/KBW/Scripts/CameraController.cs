using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private InputActionProperty triggerAction;
    [SerializeField] private GameObject testUI;

    private bool isViewfinderActive = false;
    private bool wasPressed = false;

    private void Update()
    {
        bool isPressed = triggerAction.action.IsPressed();

        if (isPressed && !wasPressed)
        {
            isViewfinderActive = !isViewfinderActive;

            if(isViewfinderActive)
            {
                Debug.Log("Camera On");
                CameraManager.Instance.EnterCameraMode();
                if(testUI != null) testUI.SetActive(true);
            }
            else
            {
                Debug.Log("Camera Off");
                CameraManager.Instance.ExitCameraMode();
                if (testUI != null) testUI.SetActive(false);
            }
        }
        wasPressed = isPressed;
    }
}
