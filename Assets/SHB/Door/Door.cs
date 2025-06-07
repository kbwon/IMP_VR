using System;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

public class Door : MonoBehaviour
{
    [Header("아무것도 건들지 말 것")]
    public Transform teleportLocation;
    public InOutDoor inOutDoor;
    public DoorSoundManager doorSoundManager;
    public TextMeshPro guideLetter;
    private GameObject player;
    private int doorNumber;  //열쇠가 필요없는 문은 0번이다.

    private bool haveKey = false;


    [Header("락커의 경우, 얘만 건들 것")]
    public bool isItLocker;
    public bool isInsideLocker;


    void Start()
    {
        player = GameObject.FindWithTag("Player");
        doorNumber = inOutDoor.doorNumber;

        if (doorNumber == 0) haveKey = true;

        isItLocker = inOutDoor.isItLocker;
    }

    public void grabDoorHandle()
    {

        if (isDoorCollectWithMyKey() == false)
        {
            doorSoundManager.cannotOpenSoundPlay();
            haveKey = false;
            changeGuideLetter();
            return;
        }

        player.transform.position = teleportLocation.position;
        if (isItLocker == true && isInsideLocker == false) player.transform.rotation = teleportLocation.rotation;

        int firstPlayerWhere = 0;
        if (isItLocker == false && inOutDoor.isIn == false) PlayerInfo.Instance.playerWhere = doorNumber;
        else if (isItLocker == false && inOutDoor.isIn == true) PlayerInfo.Instance.playerWhere = 0;

        else if (isItLocker == true && inOutDoor.isIn == false)
        {
            firstPlayerWhere = PlayerInfo.Instance.playerWhere;
            PlayerInfo.Instance.playerWhere = 99;
        }

        else if (isItLocker == true && inOutDoor.isIn == true)
        {
            PlayerInfo.Instance.playerWhere = firstPlayerWhere;
        }

        PlayerInfo.Instance.printPlayerWhere();

        inOutDoor.isIn = !inOutDoor.isIn;  // 실내/실외 상태 업데이트
        inOutDoor.doorUpdate();
        doorSoundManager.canOpenSoundPlay();
    }

    public bool isDoorCollectWithMyKey()  // 열쇠랑 문 번호가 맞는지 체크를 함.
    {
        foreach (int keyNumber in PlayerInfo.Instance.keyNumberList)
        {
            Debug.Log("플레이어가 가진 키: " + keyNumber);
            if (doorNumber == keyNumber)
            {
                haveKey = true;
                return true;
            }
        }

        return false;
    }

    public void changeGuideLetter()
    {
        isDoorCollectWithMyKey();

        if (haveKey == true) guideLetter.text = "Press Grab to open";
        else guideLetter.text = "You don't have key.";

        if (isItLocker == true && isInsideLocker == false) guideLetter.text = "Press Grab to hide";
        else if (isItLocker == true && isInsideLocker == true) guideLetter.text = "Press grab to go outside";
    }

}
