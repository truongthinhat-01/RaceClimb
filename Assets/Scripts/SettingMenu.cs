using UnityEngine;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    [Header("UI")]
    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {
        // Load volume đã lưu (mặc định = 1)
        float savedMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float savedSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

        if (musicSlider != null)
        {
            musicSlider.value = savedMusicVolume;
            musicSlider.onValueChanged.AddListener(SetMusicVolume);
        }

        if (sfxSlider != null)
        {
            sfxSlider.value = savedSFXVolume;
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        }

        // Gán giá trị vào SoundManager
        SoundManager.instance.SetMusicVolume(savedMusicVolume);
        SoundManager.instance.SetSFXVolume(savedSFXVolume);
    }

    public void IncreaseMusicVolume()
    {
        float newVolume = Mathf.Clamp(PlayerPrefs.GetFloat("MusicVolume", 1f) + 0.1f, 0f, 1f);
        SoundManager.instance.SetMusicVolume(newVolume);
        PlayerPrefs.SetFloat("MusicVolume", newVolume);
        if (musicSlider != null)
            musicSlider.value = newVolume;
    }

    public void DecreaseMusicVolume()
    {
        float newVolume = Mathf.Clamp(PlayerPrefs.GetFloat("MusicVolume", 1f) - 0.1f, 0f, 1f);
        SoundManager.instance.SetMusicVolume(newVolume);
        PlayerPrefs.SetFloat("MusicVolume", newVolume);
        if (musicSlider != null)
            musicSlider.value = newVolume;
    }

    public void MusicOn()
    {
        SoundManager.instance.MusicOn();
    }

    public void MusicOff()
    {
        SoundManager.instance.MusicOff();
    }

    public void SetMusicVolume(float value)
    {
        SoundManager.instance.SetMusicVolume(value);
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    public void SetSFXVolume(float value)
    {
        SoundManager.instance.SetSFXVolume(value);
        PlayerPrefs.SetFloat("SFXVolume", value);
    }

    void OnDisable()
    {
        PlayerPrefs.Save(); // Lưu lại khi đóng SettingMenu
    }
}
