using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InOutDoor : MonoBehaviour
{
    public GameObject inDoor; // The door object used when the player is inside
    public GameObject outDoor; // The door object used when the player is outside
    public bool isIn = false; // Player starts outside

    // (Only modify the item below)
    [Header("아래 항목들만 손댈 것")]
    public int doorNumber = 0; // Unique number assigned to this door

    // Initializes the door: show outside door and hide inside door
    void Start()
    {
        outDoor.SetActive(true);
        inDoor.SetActive(false);
    }

    // Toggles between in/out door states and schedules delayed door activation
    public void doorUpdate()
    {
        if (isIn == true)
        {
            outDoor.SetActive(false);
            StartCoroutine(DelayedOutDoorUpdate(0.5f));
        }

        else
        {
            inDoor.SetActive(false);
            StartCoroutine(DelayedOutDoorUpdate(0.5f));
        }
    }

    // Reactivates the appropriate door after a short delay
    private IEnumerator DelayedOutDoorUpdate(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (isIn == true)
        {
            inDoor.SetActive(true);
        }

        else
        {
            outDoor.SetActive(true);
        }
    }
}
