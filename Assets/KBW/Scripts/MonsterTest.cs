using UnityEngine;

public class MonsterTest : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            PlayerInfo.Instance.isDead = true;
            PlayerInfo.Instance.whenPlayerDied();
            Debug.Log("����");
        }
    }
}
