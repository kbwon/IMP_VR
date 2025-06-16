using UnityEngine;

public class LastRun : MonoBehaviour
{
    public GameObject lastBookhead; // The final Bookhead monster to activate

    void Awake()
    {
        // Ensure the Bookhead is inactive at the start
        lastBookhead.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        // Trigger the Bookhead chase when the player enters the collider
        if (other.gameObject.CompareTag("Player"))
        {
            setBookheadStart();
            Destroy(gameObject); // Prevent this from triggering again
        }
    }

    // Activates the Bookhead and updates player chase status
    public void setBookheadStart()
    {
        lastBookhead.SetActive(true);
        PlayerInfo.Instance.isPlayerChased = true;
        PlayerInfo.Instance.chasedByBookhead = true;
    }
}
