using UnityEngine;
using UnityEngine.UIElements;

public class testStatue : MonoBehaviour
{
    public bool isStared = false; 

    void Update()
    {

        if (!isStared)
        {
            transform.Rotate(new Vector3(0, 10, 0));
        }
    }
}
