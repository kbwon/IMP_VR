using UnityEngine;

public class WindowMonsterShowTrigger : MonoBehaviour
{
    public BookheadMover monster;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            monster.ActivateAndMove();
            Destroy(gameObject);
        }
    }
}
