using UnityEngine;

public class LastRun : MonoBehaviour
{
    public GameObject lastBookhead;

    void Awake()
    {
        lastBookhead.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            setBookheadStart();
            Destroy(gameObject);
        }
    }

    public void setBookheadStart()
    {
        lastBookhead.SetActive(true);
        PlayerInfo.Instance.isPlayerChased = true;
        PlayerInfo.Instance.chasedByBookhead = true;
    }
}
