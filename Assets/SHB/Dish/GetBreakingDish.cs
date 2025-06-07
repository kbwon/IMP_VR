using System.Collections.Generic;
using UnityEngine;

public class GetBreakingDish : MonoBehaviour
{
    public void getBreakingDish(GameObject piece)
    {
        PlayerInfo.Instance.items.Add("DishFragment");
        PlayerInfo.Instance.printAll();

        // 잡힌 조각 제거
        Destroy(piece);
    }
}
