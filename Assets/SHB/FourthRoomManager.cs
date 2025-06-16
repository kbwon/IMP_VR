using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class FourthRoomManager : MonoBehaviour
{
    public Transform dollMoveTransform; // The position to move the doll to
    private bool playOnce = false;

    // Called when the player exits the room while the doll is active
    public void playerGoOut()
    {
        if (GameManager.Instance.dollActive == true)
        {
            if (playOnce == true) return;
            CanEscapeManager.Instance.canEscape = true;
            StartCoroutine(dollMove());
        }
    }

    // Moves the doll monster to a new position with a delay
    public IEnumerator dollMove()
    {
        yield return new WaitForSeconds(2f);
        GameManager.Instance.dollMonsterObject.GetComponent<NavMeshAgent>().speed = 3f;
        GameManager.Instance.dollMonsterObject.SetActive(false);
        GameManager.Instance.dollMonsterObject.transform.position = dollMoveTransform.position;
        GameManager.Instance.dollMonsterObject.SetActive(true);
        playOnce = true;
    }
}
