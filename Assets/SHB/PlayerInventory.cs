using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<int> keyNumberList = new();

    void Start()
    {
        keyNumberList.Add(0);
    }

}
