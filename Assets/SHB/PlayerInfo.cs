using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo Instance { get; private set; }

    public GameObject hud;
    public List<int> keyNumberList = new();
    public List<string> items = new();
    public bool camcoder = false;
    public int playerWhere = 0; //복도
    public bool isPlayerChased = false;
    public bool chasedByBookhead = false;
    public bool chasedByDoll = false;

    void Start()
    {
        hud.SetActive(false);
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // 씬 전환 시 유지하려면

        keyNumberList.Add(1);
        keyNumberList.Add(4); //***테스트용
        keyNumberList.Add(5);  //****테스트 용으로 추가
        items.Add("DishFragment"); //****테스트 용
        keyNumberList.Add(99);
    }

    public void printAll()
    {
        Debug.Log("Items: " + string.Join(", ", items));
        Debug.Log("Key Numbers: " + string.Join(", ", keyNumberList));
    }

    public void printPlayerWhere()
    {
        Debug.Log("Player in " + playerWhere);
    }
}
