using UnityEngine;

public class FollowSocket : MonoBehaviour
{
    [SerializeField]
    private Vector3 offset; // Offset to apply relative to the main camera

    void Update()
    {
        // Update this object's position based on the main camera position plus offset
        transform.position = Camera.main.transform.position +
                             (offset.x * Vector3.right) +
                             (offset.y * Vector3.up) +
                             (offset.z * Vector3.forward);
    }
}
