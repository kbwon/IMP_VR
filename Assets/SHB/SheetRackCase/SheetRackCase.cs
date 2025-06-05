using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SheetRackCase : MonoBehaviour
{
    public bool isOpen = false;
    public TextMeshPro guideletter;
    public XRGrabInteractable xrGrab;
    public Sounds sound;
    private bool isMoving = false;  // ⭐ 움직이는 중인지 확인

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private Coroutine moveCoroutine;

    void Start()
    {
        closedPosition = transform.localPosition;
        openPosition = closedPosition + new Vector3(0f, 0f, -0.428f);

        if (isOpen == true) transform.localPosition += new Vector3(0f, 0f, -0.428f);
    }

    public void pressSheetRack()
    {
        if (isMoving)
        {
            return;
        }

        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);

        Vector3 target = isOpen ? closedPosition : openPosition;
        moveCoroutine = StartCoroutine(MoveToPosition(target));

        isOpen = !isOpen;

        if (isOpen == false) guideletter.text = "Press Grab to open";
        else guideletter.text = "Press Grab to close";

        sound.PlayRandomSound();
    }

    IEnumerator MoveToPosition(Vector3 target)
    {
        isMoving = true;  // ✅ 시작할 때 잠금
        xrGrab.enabled = false;
        float duration = 0.8f;
        float elapsed = 0f;
        Vector3 start = transform.localPosition;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            transform.localPosition = Vector3.Lerp(start, target, t);
            yield return null;
        }

        transform.localPosition = target;
        isMoving = false;  // ✅ 끝나면 다시 허용
        xrGrab.enabled = true;
    }
}
