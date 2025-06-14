using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Toilet : MonoBehaviour
{
    public BathAndToiletManager bathAndToiletManager;
    public GameObject guideLetter;
    public Sounds sounds;
    public int toiletNumber;

    public void grabToilet()
    {
        sounds.PlayRandomSound();
        bathAndToiletManager.makeDrainBath(toiletNumber);
        Destroy(guideLetter);
        Destroy(gameObject.GetComponent<XRGrabInteractable>());
    }
}
