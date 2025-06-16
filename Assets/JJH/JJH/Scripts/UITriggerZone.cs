using UnityEngine;

public class UITriggerZone : MonoBehaviour
{
    [Header("UI Objects")]
    public GameObject firstUI;   // Default UI that appears
    public GameObject secondUI;  // Additional UI shown on Space key press

    private bool isPlayerInside = false;

    private void Start()
    {
        firstUI.SetActive(false);
        secondUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
            firstUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
            firstUI.SetActive(false);
            secondUI.SetActive(false);
        }
    }

    private void Update()
    {
        if (isPlayerInside && Input.GetKeyDown(KeyCode.Space))
        {
            secondUI.SetActive(true);
        }
    }
}
