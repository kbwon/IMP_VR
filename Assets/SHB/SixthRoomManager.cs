using UnityEngine;
using System.Collections;

public class SixthRoomManager : MonoBehaviour
{
    public RuntimeAnimatorController idleBookhead;
    public RuntimeAnimatorController originalBookhead;
    private bool firstOpen = true;
    private bool firstOut = true;
    public Transform newTransform;
    public GameObject keyNumber7;

    void Start()
    {
        keyNumber7.SetActive(false);
    }

    public void sixthRoomManagerOn()
    {
        GameManager.Instance.bookheadMonsterObject.GetComponent<Animator>().runtimeAnimatorController = idleBookhead;
        GameManager.Instance.bookheadMonsterObject.GetComponent<MonsterAI>().enabled = false;
        GameManager.Instance.bookheadMonsterObject.SetActive(true);
    }

    public void openDoorFirst()
    {
        if (!firstOpen) return; // false면 아무것도 안 함

        firstOpen = false;
        StartCoroutine(DelayedBookheadAwaken()); // true일 때만 5초 후 실행
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
        GameManager.Instance.ToggleBookheadBehavior(false);

        GameManager.Instance.ToggleBookheadBehavior(true);
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

        keyNumber7.SetActive(true);
    }
}
