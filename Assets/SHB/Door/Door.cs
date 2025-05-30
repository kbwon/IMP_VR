using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    public String DoNotEditThisClass = "이 클래스 건들면 안 됨!";
    public Transform teleportLocation;
    public InOutDoor inOutDoor;
    private GameObject player;
    private int doorNumber;  //열쇠가 필요없는 문은 0번이다.
    private PlayerInventory playerInventory;


    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerInventory = player.GetComponent<PlayerInventory>();
        doorNumber = inOutDoor.doorNumber;
    }

    public void grabDoorHandle()
    {
        if (doorNumber != 0)
        {
            if (isDoorCollectWithMyKey() == false) return;
        }

        player.transform.position = teleportLocation.position;
        inOutDoor.isIn = !inOutDoor.isIn;  // 실내/실외 상태 업데이트
        inOutDoor.doorUpdate();
    }

    public bool isDoorCollectWithMyKey()  // 열쇠랑 문 번호가 맞는지 체크를 함.
    {
        foreach (int keyNumber in playerInventory.keyNumberList)
        {
            if (doorNumber == keyNumber)
            {
                Debug.Log("My key: " + keyNumber + " // Door number: " + doorNumber);
                doorNumber = 0; // 열쇠가 맞으니 더 이상 매회 열쇠 체크할 필요가 없음. 문 번호 0으로 바꿈.
                return true;
            }
        }

        return false;
    }

}
