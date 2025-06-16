using UnityEngine;

public class IfCameraUsing : MonoBehaviour
{
    public GameObject cameraOffObject; // Object to show when camera mode is off
    public GameObject cameraOnObject;  // Object to show when camera mode is on
    public bool isCameraOn = false;    // Current camera mode state

    void Update()
    {
        isCameraOn = CameraManager.Instance.isCameraMode;

        if (isCameraOn == false) cameraOff();
        else cameraOn();
    }

    // Activates camera mode objects and disables others
    public void cameraOn()
    {
        cameraOffObject.SetActive(false);
        cameraOnObject.SetActive(true);
        isCameraOn = true;
    }

    // Deactivates camera mode objects and enables others
    public void cameraOff()
    {
        cameraOffObject.SetActive(true);
        cameraOnObject.SetActive(false);
        isCameraOn = false;
    }
}
