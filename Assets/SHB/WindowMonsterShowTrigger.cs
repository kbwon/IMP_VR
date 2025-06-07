using UnityEngine;

public class WindowMonsterShowTrigger : MonoBehaviour
{
    public BookheadMover monster;
    public void OnTriggerEnter(Collider other)
    {
        monster.ActivateAndMove();
        Destroy(gameObject);
    }
}
