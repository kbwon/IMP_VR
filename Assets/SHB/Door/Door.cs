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
    private int doorNumber;

    private bool haveKey = false;
    private int firstPlayerWhere = 7777;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        doorNumber = inOutDoor.doorNumber;

        if (doorNumber == 0) haveKey = true;
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
        if (doorNumber == 99) player.transform.rotation = teleportLocation.rotation;

        if (inOutDoor.isIn == false) PlayerInfo.Instance.playerWhere = doorNumber;
        else if (inOutDoor.isIn == true) PlayerInfo.Instance.playerWhere = 0;


        PlayerInfo.Instance.printPlayerWhere();

        inOutDoor.isIn = !inOutDoor.isIn;  // 실내/실외 상태 업데이트
        inOutDoor.doorUpdate();
        doorSoundManager.canOpenSoundPlay();

        if (doorNumber == 7) Debug.Log("탈출 성공");
    }

    public bool isDoorCollectWithMyKey()  // 열쇠랑 문 번호가 맞는지 체크를 함.
    {
        foreach (int keyNumber in PlayerInfo.Instance.keyNumberList)
        {
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

        if (doorNumber == 99 && !inOutDoor.isIn) guideLetter.text = "Press Grab to hide";
        else if (doorNumber == 99 && inOutDoor.isIn) guideLetter.text = "Press grab to go outside";
    }

}
