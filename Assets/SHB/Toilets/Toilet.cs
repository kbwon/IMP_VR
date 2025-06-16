using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Toilet : MonoBehaviour
{
    public BathAndToiletManager bathAndToiletManager; // Reference to the bath manager to coordinate drains
    public GameObject guideLetter; // UI object providing player instructions
    public Sounds sounds; // Sound manager for toilet interaction
    public int toiletNumber; // Index of this toilet in the manager list

    // Called when the player interacts with the toilet
    public void grabToilet()
    {
        sounds.PlayRandomSound();
        bathAndToiletManager.makeDrainBath(toiletNumber);
        Destroy(guideLetter); // Remove UI hint
        Destroy(gameObject.GetComponent<XRGrabInteractable>()); // Disable further interaction
    }
}
