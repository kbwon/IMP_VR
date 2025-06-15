using UnityEngine;
using System.Collections;

public class Bath : MonoBehaviour
{
    public GameObject water;
    public Transform waterMoveHere1;
    public Transform waterMoveHere2;

    public void drainWater()
    {
        StartCoroutine(DrainRoutine());
    }

    IEnumerator DrainRoutine()
    {
        float halfDuration = 1.5f;
        GetComponent<Sounds>().PlayRandomSound();
        // 1단계: 현재 위치 → waterMoveHere1
        yield return StartCoroutine(MoveWater(water.transform, waterMoveHere1, halfDuration));

        // 2단계: waterMoveHere1 → waterMoveHere2
        yield return StartCoroutine(MoveWater(water.transform, waterMoveHere2, halfDuration));

        // 도착 후 오브젝트 제거
        Destroy(water);
    }

    IEnumerator MoveWater(Transform target, Transform destination, float duration)
    {
        float elapsed = 0f;

        Vector3 startPos = target.position;
        Quaternion startRot = target.rotation;
        Vector3 startScale = target.localScale;

        Vector3 endPos = destination.position;
        Quaternion endRot = destination.rotation;
        Vector3 endScale = destination.localScale;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            target.position = Vector3.Lerp(startPos, endPos, t);
            target.rotation = Quaternion.Slerp(startRot, endRot, t);
            target.localScale = Vector3.Lerp(startScale, endScale, t);

            yield return null;
        }

        // 정확하게 보정
        target.position = endPos;
        target.rotation = endRot;
        target.localScale = endScale;
    }
}
