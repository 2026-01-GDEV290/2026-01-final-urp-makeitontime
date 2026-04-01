using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    AudioMixer gameMixer;
    [SerializeField]
    Slider musicSlider;
    [SerializeField]
    Slider SFXSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("MusicVol"))
        {
            LoadMusicVolume();
        }
        else
        {
            LoadMusicVolume();
        }
        if (PlayerPrefs.HasKey("SFXVol"))
        {
            LoadSFXVolume();
        }
        else
        {
            LoadSFXVolume();
        }
    }

    public void SetMusicVol()
    {
        float volume = musicSlider.value;
        gameMixer.SetFloat("MusicVol", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVol", volume);
    }
    
    public void SetSFXVol()
    {
        float volume = SFXSlider.value;
        gameMixer.SetFloat("SFXVol", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVol", volume);
    }
    public void LoadMusicVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVol");

        SetMusicVol();
    }
    public void LoadSFXVolume()
    {
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVol");

        SetSFXVol();
    }
}
