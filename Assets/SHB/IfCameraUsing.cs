using UnityEngine;

public class IfCameraUsing : MonoBehaviour
{
    public GameObject cameraOffObject;
    public GameObject cameraOnObject;
    public bool isCameraOn = false;

    void Update()  // 병욱이 거 구현된 거 보고 최적화 할 듯
    {
        isCameraOn = CameraManager.Instance.isCameraMode;
        if (isCameraOn == false) cameraOff();
        else cameraOn();
    }

    public void cameraOn()
    {
        cameraOffObject.SetActive(false);
        cameraOnObject.SetActive(true);
        isCameraOn = true;
    }

    public void cameraOff()
    {
        cameraOffObject.SetActive(true);
        cameraOnObject.SetActive(false);
        isCameraOn = false;
    }
}
