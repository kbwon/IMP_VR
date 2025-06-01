using System.Collections;
using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class CameraController : MonoBehaviour
{
    [SerializeField] private InputActionProperty triggerAction;
    [SerializeField] private GameObject myTunnelingVignette;
    [SerializeField] private GameObject tunnelingVignette;
    [SerializeField] private GameObject hud;
    [SerializeField] private Text activeTimeText;

    private float activeTime = 100f;
    private float coolDown = 0f;
    private bool isCameraActive = false;
    private bool wasPressed = false;

    private void Update()
    {
        bool isPressed = triggerAction.action.IsPressed();

        if (isPressed && !wasPressed)
        {
            isCameraActive = !isCameraActive;

            if(isCameraActive && activeTime > 0)
            {
                Debug.Log("Camera On");
                CameraManager.Instance.EnterCameraMode();
                if (tunnelingVignette != null) tunnelingVignette.SetActive(false);
                if (myTunnelingVignette != null) myTunnelingVignette.SetActive(true);
                if (hud != null) hud.SetActive(true);
            }
            else
            {
                Debug.Log("Camera Off");
                CameraManager.Instance.ExitCameraMode();
                if (tunnelingVignette != null) tunnelingVignette.SetActive(false);
                if (myTunnelingVignette != null) myTunnelingVignette.SetActive(true);
                if (tunnelingVignette != null) tunnelingVignette.SetActive(true);
                if (myTunnelingVignette != null) myTunnelingVignette.SetActive(false);
                if (hud != null) hud.SetActive(false);
            }
        }

        // 카메라 활성 상태면 시간 감소
        if (isCameraActive)
        {
            activeTime -= Time.deltaTime;
            if (activeTime <= 0)
            {
                Debug.Log("Camera Shut Down");
                isCameraActive = false;
                CameraManager.Instance.ExitCameraMode();
                if (tunnelingVignette != null) tunnelingVignette.SetActive(false);
                if (myTunnelingVignette != null) myTunnelingVignette.SetActive(true);
                if (hud != null) hud.SetActive(false);
                activeTime = 0;
            }
        }

        // UI 텍스트 갱신
        if (activeTimeText != null)
        {
            activeTimeText.text = Mathf.Ceil(activeTime).ToString();
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
