using UnityEngine;

public class Battery : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getBattery()
    {
        PlayerInfo.Instance.items.Add("Battery");
        PlayerInfo.Instance.printAll();
        Destroy(gameObject);
    }
}
