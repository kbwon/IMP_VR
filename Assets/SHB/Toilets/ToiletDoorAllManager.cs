using UnityEngine;

public class ToiletDoorAllManager : MonoBehaviour
{
    public int doorOpenCount = 0;
    public GameObject[] toiletDoorHandle;
    public Transform[] whereToSitDool;
    public GameObject sitDoll;
    private int leftDoorNumber = 0;

    void Start()
    {
        sitDoll.SetActive(false);
    }

    public void whenThird()
    {
        doorOpenCount++;
        if (doorOpenCount != 3) return;

        foreach (GameObject leftDoor in toiletDoorHandle)
        {
            if (leftDoor != null) break;
            leftDoorNumber++;
        }

        sitDoll.transform.position = whereToSitDool[leftDoorNumber].transform.position;
        sitDoll.transform.rotation = whereToSitDool[leftDoorNumber].transform.rotation;
        sitDoll.SetActive(true);
        doorOpenCount = 999;
    }
}
