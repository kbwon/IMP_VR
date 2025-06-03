using System;
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

    

    public void doorUpdate()
    {
        inDoor.SetActive(isIn);   // 밖에 있으면outDoor 보여줌
        outDoor.SetActive(!isIn);   // 안에 있으면 inDoor 보여줌
        
    }
}
