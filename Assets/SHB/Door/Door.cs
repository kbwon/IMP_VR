using System;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

public class Door : MonoBehaviour
{
    // (Do not modify anything in this section)
    [Header("아무것도 건들지 말 것")]
    public Transform teleportLocation; // Where the player will be teleported to
    public InOutDoor inOutDoor; // Reference to indoor/outdoor door state manager
    public DoorSoundManager doorSoundManager; // Sound manager for door interactions
    public TextMeshPro guideLetter; // UI text to display door-related messages
    private GameObject player; // Reference to the player object
    private int doorNumber; // This door's unique number

    private bool haveKey = false; // Whether the player has the right key
    private int firstPlayerWhere = 7777; // Initial player location value (not used here)

    // Initializes door state and gets references to the player and door number
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        doorNumber = inOutDoor.doorNumber;

        if (doorNumber == 0) haveKey = true;
    }

    // Called when the player grabs the door handle
    public void grabDoorHandle()
    {
        if (isDoorCollectWithMyKey() == false)
        {
            doorSoundManager.cannotOpenSoundPlay();
            haveKey = false;
            changeGuideLetter();
            return;
        }

        // Door #7 triggers game completion logic
        if (doorNumber == 7)
        {
            Destroy(GameManager.Instance.gameObject);
            Destroy(ObjectDetectManager.Instance.gameObject);
            Destroy(MonsterWhereManager.Instance.gameObject);
            Destroy(PlayerInfo.Instance.gameObject);
            Destroy(CanEscapeManager.Instance.gameObject);
            YouWinOrDied.Instance.winOrDie = 1;
            SceneManager.LoadScene("Start_Scene");
        }

        // Teleport player to the target location
        player.transform.position = teleportLocation.position;
        if (doorNumber == 99) player.transform.rotation = teleportLocation.rotation;

        // Update player location info
        if (inOutDoor.isIn == false) PlayerInfo.Instance.playerWhere = doorNumber;
        else if (inOutDoor.isIn == true) PlayerInfo.Instance.playerWhere = 0;

        PlayerInfo.Instance.printPlayerWhere();

        inOutDoor.isIn = !inOutDoor.isIn; // Toggle indoor/outdoor state
        inOutDoor.doorUpdate(); // Refresh door appearance/state
        doorSoundManager.canOpenSoundPlay(); // Play opening sound

        if (doorNumber == 7) Debug.Log("Escape successful");
    }

    // Returns true if the player has a key that matches this door's number
    public bool isDoorCollectWithMyKey()
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

    // Updates the guide text depending on key possession and door state
    public void changeGuideLetter()
    {
        isDoorCollectWithMyKey();

        if (haveKey == true) guideLetter.text = "Press Grab to open";
        else guideLetter.text = "You don't have key.";

        if (doorNumber == 99 && !inOutDoor.isIn) guideLetter.text = "Press Grab to hide";
        else if (doorNumber == 99 && inOutDoor.isIn) guideLetter.text = "Press grab to go outside";
    }
}
