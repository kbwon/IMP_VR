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
        // Set up singleton instance
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object across scenes if needed
        }
        else
        {
            Destroy(gameObject); // Remove duplicate instances
        }
    }

    void Start()
    {
        // Connect slider event
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.AddListener(SetVolume);
            SetVolume(volumeSlider.value); // Apply initial slider value
        }
        else
        {
            Debug.LogWarning("volumeSlider가 설정되지 않았습니다.");
        }

        SetVolume(1f);
        volumeSlider.value = 1f;
    }


    // Can be called from other scripts
    public void SetVolume(float value)
    {
        dB = Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat("Volume", dB);
    }
}
