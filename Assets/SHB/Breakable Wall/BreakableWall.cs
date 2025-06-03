using System;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class BreakableWall : MonoBehaviour
{
    public GameObject door;
    private GameObject wall;
    public GameObject wallCameraOn;
    public GameObject wallCameraOff;
    public AudioSource audiosource;
    public AudioClip audioclip;
    public TextMeshPro guideLetter;
    [Header("필요 시 아래 항목만 체크할 것")]
    public bool canRip = false;

    private GameObject player;
    private PlayerInventory playerInventory;
    private bool hasDishFragment = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerInventory = player.GetComponent<PlayerInventory>();
        door.SetActive(false);
        guideLetter.text = string.Empty;
        wall = wallCameraOff;
        wallCameraOn.SetActive(false);

        wallCameraOn.GetComponent<XRGrabInteractable>().enabled = false;
        wallCameraOff.GetComponent<XRGrabInteractable>().enabled = false;

        if (canRip == false)
        {
            gameObject.GetComponent<IfCameraUsing>().enabled = false;
        }
    }

    void Update()
    {
        if (canRip == false) return;
        if (hasDishFragment) return;

        if (InventoryHasItem("DishFragment"))
        {
            hasDishFragment = true;
            guideLetter.text = "Press Grab to use Dish Fragment to remove wallpaper";
            wallCameraOn.GetComponent<XRGrabInteractable>().enabled = true;
            wallCameraOff.GetComponent<XRGrabInteractable>().enabled = true;
        }
    }

    public void removeWall()
    {
        if (canRip == false) return;
        if (!hasDishFragment) return;

        audiosource.PlayOneShot(audioclip);
        Destroy(wallCameraOn);
        Destroy(wallCameraOff);
        Destroy(guideLetter.gameObject);
        door.SetActive(true);
    }

    private bool InventoryHasItem(string target)
    {
        foreach (string item in playerInventory.items)
        {
            if (item.Equals(target, StringComparison.Ordinal))
            {
                return true;
            }
        }
        return false;
    }
}
