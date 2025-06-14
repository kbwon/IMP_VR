using UnityEngine;

public class MonsterTest : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Á×À½");
        }
    }
}
