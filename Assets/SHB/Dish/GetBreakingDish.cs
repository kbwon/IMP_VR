using System.Collections.Generic;
using UnityEngine;

public class GetBreakingDish : MonoBehaviour
{
    public void getBreakingDish(GameObject piece)
    {
        PlayerInventory.items.Add("DishFragment");
        PlayerInventory.printAll();

        // 잡힌 조각 제거
        Destroy(piece);
    }
}
