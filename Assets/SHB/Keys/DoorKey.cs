using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class DoorKey : MonoBehaviour
{
    public int keyNumber = 1; // The unique ID of the key
    public bool isAlwaysShow = true; // If true, key is always visible regardless of camera
    private GameObject player; // Reference to the player object
    private IfCameraUsing ifCameraUsing; // Script handling camera-based visibility

    public AudioClip pickupSoundclip; // Sound clip played when the key is picked up

    // Initializes camera-based visibility logic for the key
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        ifCameraUsing = this.gameObject.GetComponent<IfCameraUsing>();

        if (isAlwaysShow == true)
        {
            ifCameraUsing.enabled = false;
            ifCameraUsing.cameraOn();
        }
        else
        {
            ifCameraUsing.cameraOff();
        }
    }

    // Enables or disables grab interaction based on camera state
    void Update()
    {
        if (isAlwaysShow == true) return;

        if (ifCameraUsing.isCameraOn == true)
        {
            this.gameObject.GetComponent<XRGrabInteractable>().enabled = true;
        }
        else
        {
            this.gameObject.GetComponent<XRGrabInteractable>().enabled = false;
        }
    }

    // Called when the player picks up the key
    public void PlayerGetKey()
    {
        // 1. Add key number to the player's inventory
        PlayerInfo.Instance.keyNumberList.Add(keyNumber);

        // 2. Create a temporary sound object at the key's position
        GameObject soundObject = new GameObject("KeyPickupSound");
        soundObject.transform.position = transform.position;

        // 3. Add and configure AudioSource
        AudioSource audioSource = soundObject.AddComponent<AudioSource>();
        audioSource.clip = pickupSoundclip;
        audioSource.outputAudioMixerGroup = AssetDatabase.LoadAssetAtPath<AudioMixerGroup>("Assets/MainMixer.mixer");
        audioSource.playOnAwake = false;

        // 4. Play sound and destroy the object after it finishes
        audioSource.Play();
        Destroy(soundObject, pickupSoundclip.length + 0.1f);

        // 5. Destroy the key object
        Destroy(this.gameObject);
    }
}
