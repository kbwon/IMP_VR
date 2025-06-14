using System;
using System.Collections.Generic;
using UnityEngine;

public class GetBreakingDish : MonoBehaviour
{
    public void getBreakingDish(GameObject piece)
    {
        foreach (String itemName in PlayerInfo.Instance.items)
        {
            if (itemName.Equals("DishFragment")){
                Destroy(piece);
                return;
            }
        }
        PlayerInfo.Instance.items.Add("DishFragment");
        PlayerInfo.Instance.printAll();

        // 잡힌 조각 제거
        Destroy(piece);
    }
}
