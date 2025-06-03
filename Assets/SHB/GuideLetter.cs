using UnityEngine;

public class GuideLetter : MonoBehaviour
{
    public GameObject guideLetter;

    public void guideLetterOn()
    {
        guideLetter.SetActive(true);
    }

    public void guideLetterOff()
    {
        guideLetter.SetActive(false);
    }
}
