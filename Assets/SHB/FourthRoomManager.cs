using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class FourthRoomManager : MonoBehaviour
{
    public Transform dollMoveTransform;
    public void playerGoOut()  // 플레이어가 인형이 활성화 되었을 때 방 밖으로 나가게 되면
    {
        if (GameManager.Instance.dollActive == true)
        {
            CanEscapeManager.Instance.canEscape = true;
            StartCoroutine(dollMove());
        }
    }

    public IEnumerator dollMove()
    {
        yield return new WaitForSeconds(2f);
        GameManager.Instance.dollMonsterObject.GetComponent<NavMeshAgent>().speed = 3f;
        GameManager.Instance.dollMonsterObject.SetActive(false);
        GameManager.Instance.dollMonsterObject.transform.position = dollMoveTransform.position;
        GameManager.Instance.dollMonsterObject.SetActive(true);
    }
}
