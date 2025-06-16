using UnityEngine;
using System.Collections;

public class Bath : MonoBehaviour
{
    public GameObject water; // The water object to be drained
    public Transform waterMoveHere1; // Intermediate position for drain animation
    public Transform waterMoveHere2; // Final position where water disappears

    // Starts the water drain animation sequence
    public void drainWater()
    {
        StartCoroutine(DrainRoutine());
    }

    // Coordinates the two-phase water drain process
    IEnumerator DrainRoutine()
    {
        float halfDuration = 1.5f;
        GetComponent<Sounds>().PlayRandomSound();

        // Phase 1: move water from current position to waterMoveHere1
        yield return StartCoroutine(MoveWater(water.transform, waterMoveHere1, halfDuration));

        // Phase 2: move water from waterMoveHere1 to waterMoveHere2
        yield return StartCoroutine(MoveWater(water.transform, waterMoveHere2, halfDuration));

        // Destroy the water object after animation completes
        Destroy(water);
    }

    // Smoothly moves the water object to a target position, rotation, and scale
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

        // Snap to exact final values
        target.position = endPos;
        target.rotation = endRot;
        target.localScale = endScale;
    }
}
