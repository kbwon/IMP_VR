using UnityEngine;

public class MonsterTest : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger is the player
        if (other.gameObject.tag == "Player")
        {
            PlayerInfo.Instance.isDead = true; // Mark player as dead
            PlayerInfo.Instance.whenPlayerDied(); // Call method to handle player death
            Debug.Log("Dead");
        }
    }
}
