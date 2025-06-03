using UnityEngine;

public class GuideLetter : MonoBehaviour
{
    public GameObject guideLetter;
    [Header("필요 시 다음 항목 체크할 것")]
    public bool isFaceToCamera = false;

    void Start()
    {
        //guideLetter.SetActive(false);
        guideLetterOff();
    }

    public void guideLetterOn()
    {
        //guideLetter.SetActive(true);
        guideLetter.GetComponent<Renderer>().enabled = true;
    }

    public void guideLetterOff()
    {
        guideLetter.GetComponent<Renderer>().enabled = false;
        //guideLetter.SetActive(false);
    }

    void Update()
{
    if (isFaceToCamera)
    {
        Transform cam = Camera.main.transform;

        // 카메라 방향을 바라보게 회전
        guideLetter.transform.LookAt(cam);

        // 시선이 반대가 되지 않도록 180도 회전
        guideLetter.transform.Rotate(0, 180f, 0);
    }
}
}
