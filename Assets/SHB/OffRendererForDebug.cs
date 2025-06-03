using UnityEngine;

public class OffRendererForDebug : MonoBehaviour
{
    void Start()
{
    if (Application.isPlaying)
    {
        GetComponent<Renderer>().enabled = false;
    }
}

}
