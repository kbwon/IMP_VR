using UnityEngine;

public class MonsterTest : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            PlayerInfo.Instance.isDead = true;
            Debug.Log("Á×À½");
        }
    }
}
