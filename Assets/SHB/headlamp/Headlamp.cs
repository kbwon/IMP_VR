using UnityEngine;

public class Headlamp : MonoBehaviour
{
    public void getHeadlamp()
    {
        PlayerInventory.items.Add("Headlamp");
        PlayerInventory.printAll();
        Destroy(gameObject);
    }
}
