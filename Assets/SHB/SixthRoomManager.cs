using UnityEngine;
using System.Collections;

public class SixthRoomManager : MonoBehaviour
{
    public RuntimeAnimatorController idleBookhead;       // Bookhead's idle animation
    public RuntimeAnimatorController originalBookhead;   // Bookhead's chase animation
    private bool firstOpen = true;  // Whether the door has been opened for the first time
    private bool firstOut = true;   // Whether the player has exited for the first time
    public Transform newTransform;  // Position to teleport Bookhead to
    public GameObject keyNumber7;   // Key object to move later
    public Transform keyNewPosition; // New position for the key

    void Start()
    {
        keyNumber7.SetActive(true);
    }

    public void sixthRoomManagerOn()
    {
        GameManager.Instance.bookheadMonsterObject.GetComponent<Animator>().runtimeAnimatorController = idleBookhead;
        GameManager.Instance.bookheadMonsterObject.GetComponent<MonsterAI>().enabled = false;
        GameManager.Instance.bookheadMonsterObject.SetActive(true);
        Debug.Log("Is this being called?");
    }

    public void openDoorFirst()
    {
        if (!firstOpen) return;  // If already triggered once, do nothing

        firstOpen = false;
        StartCoroutine(DelayedBookheadAwaken());  // Trigger after 5 seconds
    }

    public void goOutDoorFirst()
    {
        if (!firstOut) return;

        firstOut = false;
        StopAllCoroutines();

        StartCoroutine(DelayedBookheadFollowPlayer());
        CanEscapeManager.Instance.canEscape = true;
    }

    private IEnumerator DelayedBookheadAwaken()
    {
        yield return new WaitForSeconds(5f);

        GameManager.Instance.bookheadMonsterObject.GetComponent<Animator>().runtimeAnimatorController = originalBookhead;
        GameManager.Instance.bookheadMonsterObject.GetComponent<MonsterAI>().enabled = true;
        GameManager.Instance.bookheadMonsterObject.SetActive(false);
        GameManager.Instance.ToggleBookheadBehavior(false); // Ensure clean reset

        GameManager.Instance.ToggleBookheadBehavior(true);  // Start chasing
        PlayerInfo.Instance.isPlayerChased = true;
        PlayerInfo.Instance.chasedByBookhead = true;
    }

    private IEnumerator DelayedBookheadFollowPlayer()
    {
        yield return new WaitForSeconds(2f);

        GameManager.Instance.bookheadMonsterObject.GetComponent<Animator>().runtimeAnimatorController = originalBookhead;
        GameManager.Instance.bookheadMonsterObject.GetComponent<MonsterAI>().enabled = true;
        GameManager.Instance.bookheadMonsterObject.SetActive(false);
        GameManager.Instance.ToggleBookheadBehavior(false);

        GameManager.Instance.bookheadMonsterObject.transform.position = newTransform.position;
        GameManager.Instance.bookheadMonsterObject.transform.rotation = newTransform.rotation;

        GameManager.Instance.ToggleBookheadBehavior(true);
        PlayerInfo.Instance.isPlayerChased = true;
        PlayerInfo.Instance.chasedByBookhead = true;

        keyNumber7.transform.position = keyNewPosition.position;
    }
}
