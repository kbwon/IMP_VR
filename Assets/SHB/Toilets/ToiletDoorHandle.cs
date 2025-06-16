using UnityEngine;
using System.Collections;

public class ToiletDoorHandle : MonoBehaviour
{
    public ToiletDoorAllManager toiletDoorAllManager; // Manager that tracks door openings and triggers the doll
    public GameObject toiletDoor; // The door object to be rotated
    public Sounds sound; // Sound player for door interaction

    private bool onlyOnce = false; // Ensures the door is only opened once

    // Called when the player grabs the toilet door handle
    public void grabHandle()
    {
        if (onlyOnce == false)
        {
            onlyOnce = true;
            toiletDoorAllManager.whenThird(); // Notify manager this door has been opened
            StartCoroutine(RotateDoor()); // Start door opening animation
        }
    }

    // Animates the door rotation and then destroys this handle object
    IEnumerator RotateDoor()
    {
        sound.PlayRandomSound();
        float duration = 1f;
        float elapsed = 0f;

        Quaternion startRotation = toiletDoor.transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0f, -80f, 0f); // Rotate -80° around Y axis

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            // Ease-in curve
            float easedT = t * t;

            toiletDoor.transform.rotation = Quaternion.Slerp(startRotation, endRotation, easedT);
            yield return null;
        }

        // Snap to exact final rotation
        toiletDoor.transform.rotation = endRotation;

        // Remove this handle after door has fully opened
        Destroy(gameObject);
    }
}
