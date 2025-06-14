using UnityEngine;

public class WhenGrabLamp : MonoBehaviour
{
    public GameObject headDrop;
    public CheckSocket checkSocket;

    void Start()
    {
        headDrop = GameObject.FindWithTag("HeadDrop");
        if (headDrop == null) Debug.Log("머리에 그거 없다");

        else
        {
            checkSocket = headDrop.GetComponent<CheckSocket>();
        }
    }
    public void putOffHeadlamp()
    {
        this.transform.SetParent(null);
        checkSocket = headDrop.GetComponent<CheckSocket>();
        checkSocket.hasTriggered = false;
    }
}
