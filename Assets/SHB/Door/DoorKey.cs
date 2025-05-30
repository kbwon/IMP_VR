using System.Security.Cryptography;
using UnityEngine;

public class DoorKey : MonoBehaviour
{
    public int keyNumber = 1;
    private GameObject player;
    private PlayerInventory playerInventory;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerInventory = player.GetComponent<PlayerInventory>();
    }

    public void PlayerGetKey()  //플레이어가 키를 획득했다
    {
        playerInventory.keyNumberList.Add(keyNumber); //플레이어인벤토리의 keyList에 키넘버를 직접 추가.
        Destroy(this.gameObject);  //열쇠는 그냥 파괴.
    }
}
