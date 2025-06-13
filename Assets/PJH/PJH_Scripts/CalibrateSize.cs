using UnityEngine;

// Adjusts the arm bone's length to match the distance between the shoulder (this object) and the hand (controller)
public class CalibrateSize : MonoBehaviour
{
    public Transform handTransform; // Hand/controller Transform
    public Transform armBone;       // Arm bone to scale

    void Update()
    {
        if (handTransform == null || armBone == null) return;
        float distance = Vector3.Distance(transform.position, handTransform.position);
        Vector3 newScale = armBone.localScale;
        newScale.y = distance + 0.6f;
        armBone.localScale = newScale;
    }
}
