//using UnityEngine;
//using UnityEngine.Audio;

//public class SoundManager : MonoBehaviour
//{
//    public static SoundManager instance;
//    public static AudioClip ButtonSound, Bone_Crack, StartButtonSound, BGMusic, Vehicle_Car_Engine, fuel, coin, Menu_Click;
//    public static AudioSource audiosrc;

//    void Awake()
//    {
//        audiosrc = GetComponent<AudioSource>();
//        ButtonSound = Resources.Load<AudioClip>("Button");
//        StartButtonSound = Resources.Load<AudioClip>("StartButtonSound");
//        BGMusic = Resources.Load<AudioClip>("BGMusic");
//        Vehicle_Car_Engine = Resources.Load<AudioClip>("Vehicle_Car_Engine");
//        Bone_Crack = Resources.Load<AudioClip>("Bone_Crack");
//        fuel = Resources.Load<AudioClip>("fuel");
//        coin = Resources.Load<AudioClip>("coin");
//        Menu_Click = Resources.Load<AudioClip>("Menu_Click");

//        // Play the background music on awake and set it to loop
//        audiosrc.clip = BGMusic;
//        audiosrc.loop = true;
//        audiosrc.Play();
//    }
//    public void ChangeVolume(float change)
//    {
//        float newVolume = Mathf.Clamp(audiosrc.volume + change, 0f, 1f);
//        audiosrc.volume = newVolume;
//        PlayerPrefs.SetFloat("GameVolume", newVolume);
//    }

//    public void SetVolume(float value)
//    {
//        audiosrc.volume = value;
//    }
//    public void IncreaseVolume()
//    {
//        // tăng thêm 0.1
//        SoundManager sound = Object.FindFirstObjectByType<SoundManager>();

//        if (sound != null)
//            sound.ChangeVolume(0.1f);
//    }

//    public void DecreaseVolume()
//    {
//        // giảm 0.1
//        SoundManager sound = Object.FindFirstObjectByType<SoundManager>();

//        if (sound != null)
//            sound.ChangeVolume(-0.1f);
//    }

//    public void MusicOn()
//    {
//        if (SoundManager.audiosrc != null && !SoundManager.audiosrc.isPlaying)
//            SoundManager.audiosrc.Play();
//    }

//    public void MusicOff()
//    {
//        if (SoundManager.audiosrc != null && SoundManager.audiosrc.isPlaying)
//            SoundManager.audiosrc.Pause();
//    }

//    public static void PlaySound(string clip)
//    {
//        switch (clip)
//        {
//            case "Button":
//                audiosrc.PlayOneShot(ButtonSound);
//                break;
//            case "StartButtonSound":
//                audiosrc.PlayOneShot(StartButtonSound);
//                break;
//            case "BGMusic":
//                audiosrc.PlayOneShot(BGMusic);
//                break;
//            case "Vehicle_Car_Engine":
//                audiosrc.PlayOneShot(Vehicle_Car_Engine);
//                break;
//            case "Bone_Crack":
//                audiosrc.PlayOneShot(Bone_Crack);
//                break;
//            case "fuel":
//                audiosrc.PlayOneShot(fuel);
//                break;
//            case "coin":
//                audiosrc.PlayOneShot(coin);
//                break;
//            case "Menu_Click":
//                audiosrc.PlayOneShot(Menu_Click);
//                break;
//        }
//    }
//}


using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("Audio Clips")]
    public AudioClip ButtonSound;
    public AudioClip Bone_Crack;
    public AudioClip StartButtonSound;
    public AudioClip BGMusic;
    public AudioClip Vehicle_Car_Engine;
    public AudioClip fuel;
    public AudioClip coin;
    public AudioClip Menu_Click;
    public AudioClip Win;
    public AudioClip Loss;
    public AudioClip CarSound;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource; // để phát nhạc nền
    [SerializeField] private AudioSource sfxSource;   // để phát hiệu ứng âm thanh

    private void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Play background music on awake
        if (BGMusic != null && musicSource != null)
        {
            musicSource.clip = BGMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    #region Music Control
    public void MusicOn()
    {
        if (!musicSource.isPlaying)
            musicSource.Play();
    }

    public void MusicOff()
    {
        if (musicSource.isPlaying)
            musicSource.Pause();
    }

    public void SetMusicVolume(float value)
    {
        musicSource.volume = Mathf.Clamp01(value);
        PlayerPrefs.SetFloat("MusicVolume", musicSource.volume);
    }

    public void ChangeMusicVolume(float delta)
    {
        SetMusicVolume(musicSource.volume + delta);
    }
    #endregion

    #region SFX Control
    public void PlaySound(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
            sfxSource.PlayOneShot(clip);
    }

    public static void PlaySound(string clipName)
    {
        AudioClip clip = clipName switch
        {
            "Button" => instance.ButtonSound,
            "StartButtonSound" => instance.StartButtonSound,
            "Vehicle_Car_Engine" => instance.Vehicle_Car_Engine,
            "Bone_Crack" => instance.Bone_Crack,
            "fuel" => instance.fuel,
            "coin" => instance.coin,
            "Win" => instance.Win,
            "Loss" => instance.Loss,
            "CarSound" =>instance.CarSound,
            "Menu_Click" => instance.Menu_Click,
            _ => null
        };

        if (clip != null && instance.sfxSource != null)
            instance.sfxSource.PlayOneShot(clip);
    }

    public void SetSFXVolume(float value)
    {
        sfxSource.volume = Mathf.Clamp01(value);
        PlayerPrefs.SetFloat("SFXVolume", sfxSource.volume);
    }

    public void ChangeSFXVolume(float delta)
    {
        SetSFXVolume(sfxSource.volume + delta);
    }
    #endregion
}

