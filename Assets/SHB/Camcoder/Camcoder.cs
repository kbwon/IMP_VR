using UnityEngine;

public class Camcoder : MonoBehaviour
{
    public void getCamcoder()
    {
        PlayerInfo.Instance.camcoder = true;
        Destroy(gameObject);
    }
}
