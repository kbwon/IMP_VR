using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

// Controls game volume using a UI Slider and AudioMixer
public class VolumeControl : MonoBehaviour
{
    public Slider volumeSlider;      // Assign in Inspector
    public AudioMixer audioMixer;    // Assign in Inspector

    void Start()
    {
        // Connect the slider's value change event to SetVolume
        volumeSlider.onValueChanged.AddListener(SetVolume);

        // Set initial volume based on the slider's starting value
        SetVolume(volumeSlider.value);
    }

    // Called when the slider value changes
    public void SetVolume(float value)
    {
        // Convert 0~1 value to decibels for AudioMixer
        float dB = Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat("Volume", dB);
    }
}
