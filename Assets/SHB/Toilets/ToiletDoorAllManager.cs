using UnityEngine;

public class ToiletDoorAllManager : MonoBehaviour
{
    public int doorOpenCount = 0; // Tracks how many toilet doors have been opened
    public GameObject[] toiletDoorHandle; // References to all toilet door handles
    public Transform[] whereToSitDool; // Target positions for placing the sitting doll
    public GameObject sitDoll; // The doll that appears on the third door open
    private int leftDoorNumber = 0; // Index used to find the first non-null door

    // Hides the doll at the start
    void Start()
    {
        sitDoll.SetActive(false);
    }

    // Called whenever a toilet door is opened
    public void whenThird()
    {
        doorOpenCount++;
        if (doorOpenCount != 3) return;

        // Find the first door handle that still exists
        foreach (GameObject leftDoor in toiletDoorHandle)
        {
            if (leftDoor != null) break;
            leftDoorNumber++;
        }

        // Place and activate the doll at the corresponding toilet position
        sitDoll.transform.position = whereToSitDool[leftDoorNumber].transform.position;
        sitDoll.transform.rotation = whereToSitDool[leftDoorNumber].transform.rotation;
        sitDoll.SetActive(true);
        doorOpenCount = 999; // Prevents this block from executing again
    }
}
