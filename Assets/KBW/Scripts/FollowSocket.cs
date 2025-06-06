using UnityEngine;

public class FollowSocket : MonoBehaviour
{
    [SerializeField]
    private Vector3 offset;

    void Update()
    {
        transform.position = Camera.main.transform.position + 
                             (offset.x * Vector3.right) + 
                             (offset.y * Vector3.up) + 
                             (offset.z * Vector3.forward);
    }
}
