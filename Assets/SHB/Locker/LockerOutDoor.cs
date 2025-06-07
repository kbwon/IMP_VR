using System;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

public class LockerOutDoor : MonoBehaviour
{
    [Header("아무것도 건들지 말 것")]
    public Locker locker;
    public Transform teleportLocation;
    public DoorSoundManager doorSoundManager;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    public void grabDoorHandle()
    {
        player.transform.position = teleportLocation.position;
        player.transform.rotation = teleportLocation.rotation;

        PlayerInfo.Instance.playerWhere = 99;

        PlayerInfo.Instance.printPlayerWhere();
        doorSoundManager.canOpenSoundPlay();

        locker.isIn = true;
        locker.updateDoor();
    }
}
