using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static List<int> keyNumberList = new();
    public static List<String> items = new();
    public static void printAll()
    {
         Debug.Log("Items: " + string.Join(", ", items));
        Debug.Log("Key Numbers: " + string.Join(", ", keyNumberList));
    }

}
