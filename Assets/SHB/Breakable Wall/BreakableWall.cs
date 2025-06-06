using System;
using System.Collections;
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
    public AudioClip audioclip1;
    public AudioClip audioclip2;
    public TextMeshPro guideLetter;
    [Header("필요 시 아래 항목만 체크할 것")]
    public bool canRip = false;
    private bool hasDishFragment = false;

    public Animator wallAnimator; // Animator를 연결할 변수

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

        //audiosource.PlayOneShot(audioclip);
        gameObject.GetComponent<IfCameraUsing>().enabled = false;
        StartCoroutine(PlayTearAnimationAndRemove());
    }

    private IEnumerator PlayTearAnimationAndRemove()
    {
        if (wallAnimator != null)
        {
            wallAnimator.ResetTrigger("StartRip"); // 혹시 남아있을 이전 트리거 초기화
            wallAnimator.SetTrigger("StartRip");

            // 🔁 현재 상태가 실제로 "Ripping wall animation"이 될 때까지 기다리기
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

    private bool InventoryHasItem(string target)
    {
        foreach (string item in PlayerInventory.items)
        {
            if (item.Equals(target, StringComparison.Ordinal))
            {
                return true;
            }
        }
        return false;
    }

    public void playSound1()
    {
        audiosource.PlayOneShot(audioclip1);
    }

    public void playSound2()
    {
        audiosource.PlayOneShot(audioclip2);
    }
}
