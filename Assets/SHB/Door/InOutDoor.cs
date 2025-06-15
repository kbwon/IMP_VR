using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InOutDoor : MonoBehaviour
{

    public GameObject inDoor;
    public GameObject outDoor;
    public bool isIn = false;  //시작할 땐 밖에 있음.

    [Header("아래 항목들만 손댈 것")]
    public int doorNumber = 0;

    void Start()
    {
        outDoor.SetActive(true);
        inDoor.SetActive(false);
    }
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
