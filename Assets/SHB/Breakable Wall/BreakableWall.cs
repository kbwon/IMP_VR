using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class BreakableWall : MonoBehaviour
{
    public GameObject door; // Door object that appears after tearing the wall
    private GameObject wall; // Currently active wall (camera off version)
    public GameObject wallCameraOn; // Wall version shown when camera is on
    public GameObject wallCameraOff; // Wall version shown when camera is off
    public AudioSource audiosource; // Audio source to play sound
    public AudioClip audioclip1; // First sound effect
    public AudioClip audioclip2; // Second sound effect
    public TextMeshPro guideLetter; // UI text to guide the player
    // (Only check the item below if needed)
    [Header("필요 시 아래 항목만 체크할 것")]
    public bool canRip = false; // Whether this wall can be torn
    private bool hasDishFragment = false; // Whether the player has a dish fragment

    public Animator wallAnimator; // Animator to control the wall tearing animation

    // Initializes wall state, disables interaction, and configures visibility
    void Start()
    {
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

    // Checks player's inventory for DishFragment and enables interaction accordingly
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

    // Called to trigger the tearing wall sequence if conditions are met
    public void removeWall()
    {
        if (canRip == false) return;
        if (!hasDishFragment) return;

        gameObject.GetComponent<IfCameraUsing>().enabled = false;
        StartCoroutine(PlayTearAnimationAndRemove());
    }

    // Plays tearing animation, removes wall objects, and activates door
    private IEnumerator PlayTearAnimationAndRemove()
    {
        if (wallAnimator != null)
        {
            wallAnimator.ResetTrigger("StartRip"); // Clear previous animation trigger
            wallAnimator.SetTrigger("StartRip"); // Trigger rip animation

            // Wait until animation state becomes active
            while (!wallAnimator.GetCurrentAnimatorStateInfo(0).IsName("Ripping wall animation"))
            {
                yield return null;
            }

            float animLength = wallAnimator.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(animLength - 0.4f);
        }

        Destroy(wallCameraOn);
        Destroy(wallCameraOff);
        Destroy(guideLetter.gameObject);
        door.SetActive(true);
    }

    // Checks if a specific item exists in the player's inventory
    private bool InventoryHasItem(string target)
    {
        foreach (string item in PlayerInfo.Instance.items)
        {
            if (item.Equals(target, StringComparison.Ordinal))
            {
                return true;
            }
        }
        return false;
    }

    // Plays the first sound effect
    public void playSound1()
    {
        audiosource.PlayOneShot(audioclip1);
    }

    // Plays the second sound effect
    public void playSound2()
    {
        audiosource.PlayOneShot(audioclip2);
    }
}
