using System.Collections.Generic;
using UnityEngine;

public class GetBreakingDish : MonoBehaviour
{
    private GameObject player;
    private PlayerInventory playerInventory;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerInventory = player.GetComponent<PlayerInventory>();
    }

    public void getBreakingDish(GameObject piece)
    {
        playerInventory.items.Add("BreakingDish");
        Debug.Log("Items: " + string.Join(", ", playerInventory.items));
        Debug.Log("Key Numbers: " + string.Join(", ", playerInventory.keyNumberList));

        // 잡힌 조각 제거
        Destroy(piece);
    }
}
