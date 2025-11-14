using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsController : MonoBehaviour
{
    [Header("UI Sliders")]
    public Slider musicSlider;
    public Slider sfxSlider;

    [Header("Audio Mixer")]
    public AudioMixer audioMixer; // Gắn AudioMixer trong Inspector

    private void Start()
    {
        // Load giá trị lưu trước đó (nếu có)
        float savedMusic = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float savedSFX = PlayerPrefs.GetFloat("SFXVolume", 1f);

        musicSlider.value = savedMusic;
        sfxSlider.value = savedSFX;

        SetMusicVolume(savedMusic);
        SetSFXVolume(savedSFX);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void SaveSettings()
    {
        PlayerPrefs.Save();
    }

    public void BackToMainMenu()
    {
        SaveSettings();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
    }
}
