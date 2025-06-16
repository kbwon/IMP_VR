using System;
using System.Collections.Generic;
using UnityEngine;

public class GetBreakingDish : MonoBehaviour
{
    // Called when a dish fragment is grabbed.
    // If the player already has "DishFragment", destroy the object.
    // Otherwise, add it to the player's inventory and destroy the object.
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

        Destroy(piece); // Remove the grabbed dish piece
    }
}
