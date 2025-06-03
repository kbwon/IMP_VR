using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class DoorKey : MonoBehaviour
{
    public int keyNumber = 1;
    public bool isAlwaysShow = true;
    private GameObject player;
    private PlayerInventory playerInventory;
    private IfCameraUsing ifCameraUsing;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerInventory = player.GetComponent<PlayerInventory>();
        ifCameraUsing = this.gameObject.GetComponent<IfCameraUsing>();

        if (isAlwaysShow == true)
        {
            ifCameraUsing.enabled = false;
            ifCameraUsing.cameraOn();
        }

        else
        {
            ifCameraUsing.cameraOff();
        }
    }

    void Update()
    {
        if (isAlwaysShow == true) return;

        if (ifCameraUsing.isCameraOn == true)
        {
            this.gameObject.GetComponent<XRGrabInteractable>().enabled = true;
        }
        else
        {
            this.gameObject.GetComponent<XRGrabInteractable>().enabled = false;
        }
    }

    public void PlayerGetKey()  //플레이어가 키를 획득했다
    {
        playerInventory.keyNumberList.Add(keyNumber); //플레이어인벤토리의 keyList에 키넘버를 직접 추가.
        Destroy(this.gameObject);  //열쇠는 그냥 파괴.
    }
}
