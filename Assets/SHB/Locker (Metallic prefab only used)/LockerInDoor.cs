using System;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

public class LockerInDoor : MonoBehaviour
{
    [Header("아무것도 건들지 말 것")]
    public Locker locker;
    public Transform teleportLocation;
    public DoorSoundManager doorSoundManager;
    private GameObject player;
    private int roomNumber;
    private bool pleaseOnlyOneShootDamnWhy = true;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        roomNumber = locker.roomNumber;
    }

    public void grabDoorHandle()
    {
    //    if (pleaseOnlyOneShootDamnWhy == false)
     //   {
    //        pleaseOnlyOneShootDamnWhy = true;
    //        return;
    //    }

        
        player.transform.position = teleportLocation.position;

        PlayerInfo.Instance.playerWhere = roomNumber;

        PlayerInfo.Instance.printPlayerWhere();
        doorSoundManager.canOpenSoundPlay();

        locker.isIn = false;
        locker.updateDoor();
        //pleaseOnlyOneShootDamnWhy = false;
    }
}
