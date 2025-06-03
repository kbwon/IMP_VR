using UnityEngine;

public class GuideLetter : MonoBehaviour
{
    public GameObject guideLetter;
    public bool isFaceToCamera = false;

    public void guideLetterOn()
    {
        guideLetter.SetActive(true);
    }

    public void guideLetterOff()
    {
        guideLetter.SetActive(false);
    }

    void Update()
{
    if (isFaceToCamera && guideLetter.activeSelf)
    {
        Transform cam = Camera.main.transform;

        // 카메라 방향을 바라보게 회전
        guideLetter.transform.LookAt(cam);

        // 시선이 반대가 되지 않도록 180도 회전
        guideLetter.transform.Rotate(0, 180f, 0);
    }
}
}
