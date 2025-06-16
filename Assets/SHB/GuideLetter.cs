using UnityEngine;

public class GuideLetter : MonoBehaviour
{
    public GameObject guideLetter;

    [Header("Check this only if the text needs to face the camera")]
    public bool isFaceToCamera = false;

    void Start()
    {
        guideLetterOff();
    }

    // Enables the guide letter renderer
    public void guideLetterOn()
    {
        guideLetter.GetComponent<Renderer>().enabled = true;
    }

    // Disables the guide letter renderer
    public void guideLetterOff()
    {
        guideLetter.GetComponent<Renderer>().enabled = false;
    }

    void Update()
    {
        if (isFaceToCamera)
        {
            Transform cam = Camera.main.transform;

            // Rotate to face the camera
            guideLetter.transform.LookAt(cam);

            // Flip 180° to avoid reversed text
            guideLetter.transform.Rotate(0, 180f, 0);
        }
    }
}
