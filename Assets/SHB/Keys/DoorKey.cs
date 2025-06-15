using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class DoorKey : MonoBehaviour
{
    public int keyNumber = 1;
    public bool isAlwaysShow = true;
    private GameObject player;
    private IfCameraUsing ifCameraUsing;

    public AudioClip pickupSoundclip;

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

    public void PlayerGetKey()  //플레이어가 키를 획득했다
    {
        // 1. 키 번호를 플레이어 인벤토리에 추가
        PlayerInfo.Instance.keyNumberList.Add(keyNumber);

        // 2. 현재 위치에 빈 오브젝트 생성
        GameObject soundObject = new GameObject("KeyPickupSound");
        soundObject.transform.position = transform.position;

        // 3. AudioSource 추가 및 설정
        AudioSource audioSource = soundObject.AddComponent<AudioSource>();
        audioSource.clip = pickupSoundclip;
        audioSource.outputAudioMixerGroup = AssetDatabase.LoadAssetAtPath<AudioMixerGroup>("Assets/MainMixer.mixer");
        audioSource.playOnAwake = false;

        // 4. 사운드 재생 후 자동 파괴
        audioSource.Play();
        Destroy(soundObject, pickupSoundclip.length + 0.1f);

        // 5. 열쇠 오브젝트 파괴
        Destroy(this.gameObject);
    }


}
