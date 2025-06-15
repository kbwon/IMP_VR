using UnityEngine;
using System.Collections;

public class ToiletDoorHandle : MonoBehaviour
{
    public ToiletDoorAllManager toiletDoorAllManager;
    public GameObject toiletDoor;
    public Sounds sound;

    private bool onlyOnce = false;

    public void grabHandle()
    {
        if (onlyOnce == false)
        {
            toiletDoorAllManager.whenThird();
            onlyOnce = true;
            StartCoroutine(RotateDoor());
        }
    }

    IEnumerator RotateDoor()
    {
        sound.PlayRandomSound();
        float duration = 1f;
        float elapsed = 0f;

        Quaternion startRotation = toiletDoor.transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0f, -80f, 0f); // Y축 -80도 회전

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            // EaseIn
            float easedT = t * t;

            toiletDoor.transform.rotation = Quaternion.Slerp(startRotation, endRotation, easedT);
            yield return null;
        }

        // 정확하게 최종 회전값으로 보정
        toiletDoor.transform.rotation = endRotation;

        // 🎯 회전 끝난 후, 이 스크립트가 붙은 오브젝트 제거
        Destroy(gameObject);
    }
}
