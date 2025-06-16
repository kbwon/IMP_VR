using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class CheckSocket : MonoBehaviour
{
    public XRSocketInteractor socketInteractor; // The XR socket to monitor
    public bool hasTriggered = false; // Prevents multiple executions

    void Update()
    {
        // Skip if the event has already been triggered
        if (hasTriggered == true) return;

        // Skip if the socket is not assigned
        if (socketInteractor == null) return;

        // Check if an object is inserted into the socket
        if (socketInteractor.hasSelection)
        {
            IXRSelectInteractable selected = socketInteractor.GetOldestInteractableSelected();

            // If the inserted object is tagged as "Headlamp", parent it to the player
            if (selected != null && selected.transform.CompareTag("Headlamp"))
            {
                Debug.Log("Headlamp is inserted into the socket!");
                hasTriggered = true;

                GameObject player = GameObject.FindWithTag("Player");

                if (player != null)
                {
                    selected.transform.SetParent(player.transform);
                }
                else
                {
                    Debug.LogWarning("No object found with tag 'Player'.");
                }
            }
        }
    }
}
