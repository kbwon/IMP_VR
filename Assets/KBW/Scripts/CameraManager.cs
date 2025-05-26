using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.SceneView;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    [SerializeField] 
    private List<GameObject> hiddenObjects;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void EnterCameraMode()
    {
        foreach (var obj in hiddenObjects)
        {
            SetMode(obj, true);
            /*Transform[] children = obj.GetComponentsInChildren<Transform>();
            foreach (Transform child in obj.transform)
            {
                if (child.CompareTag("NormalMode"))
                    child.gameObject.SetActive(false);
                else if (child.CompareTag("CameraMode"))
                    child.gameObject.SetActive(true);
            }*/
        }            
    }

    public void ExitCameraMode()
    {
        foreach (var obj in hiddenObjects)
        {
            SetMode(obj, false);
            /*Transform[] children = obj.GetComponentsInChildren<Transform>();
            foreach (Transform child in obj.transform)
            {
                if (child.CompareTag("NormalMode"))
                    child.gameObject.SetActive(true);
                else if (child.CompareTag("CameraMode"))
                    child.gameObject.SetActive(false);
            }*/
        }
    }

    private void SetMode(GameObject obj, bool isCameraMode)
    {
        Transform on = obj.transform.Find("On");
        Transform off = obj.transform.Find("Off");

        if (on != null) on.gameObject.SetActive(isCameraMode);
        if (off != null) off.gameObject.SetActive(!isCameraMode);
    }

    public void RegisterHiddenObject(GameObject obj)
    {
        if (!hiddenObjects.Contains(obj))
            hiddenObjects.Add(obj);
    }
}
