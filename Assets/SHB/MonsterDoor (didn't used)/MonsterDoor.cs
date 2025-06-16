using UnityEngine;
using System.Collections;

public class MonsterDoor : MonoBehaviour
{
    public GameObject outdoor;
    public GameObject indoor;
    public int roomNumber;

    void Start()
    {
        this.gameObject.SetActive(true);
        outdoor.SetActive(false);
        indoor.SetActive(false);
    }

    void Update()
    {
        if (PlayerInfo.Instance.isPlayerChased && PlayerInfo.Instance.playerWhere == roomNumber)
        {
            outdoor.SetActive(true);
            indoor.SetActive(true);
        }
        else if (PlayerInfo.Instance.isPlayerChased && PlayerInfo.Instance.playerWhere == 0)
        {
            if (PlayerInfo.Instance.chasedByBookhead && MonsterWhereManager.Instance.bookheadWhere == roomNumber)
            {
                outdoor.SetActive(true);
                indoor.SetActive(true);
            }
            else if (PlayerInfo.Instance.chasedByDoll && MonsterWhereManager.Instance.dollWhere == roomNumber)
            {
                outdoor.SetActive(true);
                indoor.SetActive(true);
            }
        }
        else
        {
            outdoor.SetActive(false);
            indoor.SetActive(false);
        }
    }

    public void outdoorOn()
    {
        outdoor.SetActive(false);
        StartCoroutine(ReenableAfterDelay(indoor, 4f));
    }

    public void indoorOn()
    {
        indoor.SetActive(false);
        StartCoroutine(ReenableAfterDelay(outdoor, 4f));
    }

    private IEnumerator ReenableAfterDelay(GameObject target, float delay)
    {
        target.SetActive(false); // 확실히 꺼두고
        yield return new WaitForSeconds(delay);
        target.SetActive(true);
    }
}
