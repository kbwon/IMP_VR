using UnityEngine;

public class LastRun : MonoBehaviour
{
    public Transform setBookheadStartHere;

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
        GameManager.Instance.bookheadMonsterObject.SetActive(false);
        GameManager.Instance.bookheadMonsterObject.transform.position = setBookheadStartHere.position;
        GameManager.Instance.ToggleBookheadBehavior(true);
    }
}
