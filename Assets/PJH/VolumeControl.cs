using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeControl : MonoBehaviour
{
    public static VolumeControl Instance { get; private set; }

    public Slider volumeSlider;      // Assign in Inspector
    public AudioMixer audioMixer;    // Assign in Inspector
    private float dB;

    private void Awake()
    {
        // 싱글톤 인스턴스 설정
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 필요 시 유지
        }
        else
        {
            Destroy(gameObject); // 중복 제거
        }
    }

    void Start()
    {
        // 슬라이더 이벤트 연결
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.AddListener(SetVolume);
            SetVolume(volumeSlider.value); // 초기값 반영
        }
        else
        {
            Debug.LogWarning("volumeSlider가 설정되지 않았습니다.");
        }

        SetVolume(1f);
        volumeSlider.value = 1f;
    }


    // 다른 스크립트에서도 호출 가능
    public void SetVolume(float value)
    {
        dB = Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat("Volume", dB);
    }
}
