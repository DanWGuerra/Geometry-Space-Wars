using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider volumeSlider;

    private const string VolumeKey = "MasterVolume";

    private void Start()
    {
        // Load saved volume
        float volume = PlayerPrefs.GetFloat(VolumeKey, 1f);

        volumeSlider.value = volume;
        SetVolume(volume);

        // Listen for slider changes
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        // Convert linear slider value to decibels
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);

        // Save preference
        PlayerPrefs.SetFloat(VolumeKey, volume);
        PlayerPrefs.Save();
    }

    private void OnDestroy()
    {
        volumeSlider.onValueChanged.RemoveListener(SetVolume);
    }
}