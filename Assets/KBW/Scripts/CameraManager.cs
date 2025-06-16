using System.Collections.Generic;
using NUnit.Framework.Interfaces;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEditor.SceneView;

public class CameraManager : MonoBehaviour
{
    // Singleton instance, can be accessed via CameraManager.Instance
    public static CameraManager Instance;

    // Variable to indicate if camera mode is active, used by items
    public bool isCameraMode = false;

    // List of hidden objects to manage (works fine if not too many objects)
    [SerializeField]
    private List<GameObject> hiddenObjects;

    private float maxDistance = 1000f; // Maximum distance for detection (not currently used)

    public LayerMask monsterLayer; // Layer mask for monster detection (not currently used)

    private Volume cameraFilter; // Reference to post-processing volume for camera effect

    void Awake()
    {
        if (Instance == null) Instance = this; // Initialize singleton instance
    }

    void Start()
    {
        cameraFilter = GetComponentInChildren<Volume>(); // Get post-processing volume component
    }

    public void EnterCameraMode()
    {
        isCameraMode = true;
        Debug.Log("isCameraMode: " + isCameraMode);
        cameraFilter.enabled = true; // Enable post-processing effect
        foreach (var obj in hiddenObjects)
        {
            SetMode(obj, true); // Show hidden objects in camera mode
        }
    }

    public void ExitCameraMode()
    {
        isCameraMode = false;
        Debug.Log("isCameraMode: " + isCameraMode);
        cameraFilter.enabled = false; // Disable post-processing effect
        foreach (var obj in hiddenObjects)
        {
            SetMode(obj, false); // Hide objects when exiting camera mode
        }
    }

    private void SetMode(GameObject obj, bool isCameraMode)
    {
        Transform on = obj.transform.Find("On"); // Child object named "On"
        Transform off = obj.transform.Find("Off"); // Child object named "Off"

        if (on != null) on.gameObject.SetActive(isCameraMode); // Activate "On" if in camera mode
        if (off != null) off.gameObject.SetActive(!isCameraMode); // Activate "Off" otherwise
    }

    public void RegisterHiddenObject(GameObject obj)
    {
        if (!hiddenObjects.Contains(obj))
            hiddenObjects.Add(obj); // Add object to hiddenObjects list if not already registered
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider == null) Debug.Log("No collider detected");

        if (hit.collider.gameObject.CompareTag("Monster"))
        {
            Debug.Log("Player controller collision detected with Monster");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Monster")
        {
            Debug.Log("Player trigger collision detected with Monster");
        }
    }
}
