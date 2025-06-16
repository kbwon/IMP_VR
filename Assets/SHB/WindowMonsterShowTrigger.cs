using UnityEngine;

public class WindowMonsterShowTrigger : MonoBehaviour
{
    public BookheadMover monster;

    // When the player enters the trigger zone, activate the monster's movement
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            monster.ActivateAndMove();
            Destroy(gameObject); // Remove the trigger to prevent repeat activation
        }
    }
}
