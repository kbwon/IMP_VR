using UnityEngine;

public class Camcoder : MonoBehaviour
{
    public void getCamcoder()
    {
        PlayerInventory.camcoder = true;
        Destroy(gameObject);
    }
}
