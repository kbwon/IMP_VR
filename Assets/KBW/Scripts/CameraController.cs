using System.Collections;
using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private InputActionProperty triggerAction;
    [SerializeField] private GameObject myTunnelingVignette;
    [SerializeField] private GameObject tunnelingVignette;
    [SerializeField] private GameObject layer1;
    [SerializeField] private GameObject layer2;
    [SerializeField] private GameObject layer3;
    [SerializeField] private GameObject layer4;

    private bool isCameraActive = false;
    private bool wasPressed = false;

    private void Update()
    {
        bool isPressed = triggerAction.action.IsPressed();

        if (isPressed && !wasPressed)
        {
            isCameraActive = !isCameraActive;

            if(isCameraActive)
            {
                Debug.Log("Camera On");
                CameraManager.Instance.EnterCameraMode();
                if (tunnelingVignette != null) tunnelingVignette.SetActive(false);
                if (myTunnelingVignette != null) myTunnelingVignette.SetActive(true);
                StartCoroutine(DelayHUD(layer1));
                StartCoroutine(DelayHUD(layer2));
                StartCoroutine(DelayHUD(layer3));
                StartCoroutine(DelayHUD(layer4));
            }
            else
            {
                Debug.Log("Camera Off");
                CameraManager.Instance.ExitCameraMode();
                if (tunnelingVignette != null) tunnelingVignette.SetActive(true);
                if (myTunnelingVignette != null) myTunnelingVignette.SetActive(false);
                StartCoroutine(DelayHUD(layer4));
                StartCoroutine(DelayHUD(layer3));
                StartCoroutine(DelayHUD(layer2));
                StartCoroutine(DelayHUD(layer1));
            }
        }
        wasPressed = isPressed;
    }

    IEnumerator DelayHUD(GameObject layer)
    {
        yield return null;
        if (layer.activeSelf)
        {
            layer.SetActive(false);
        }
        else
        {
            layer.SetActive(true);
        }            
    }
}
