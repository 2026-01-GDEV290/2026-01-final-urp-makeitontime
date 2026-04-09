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
            SFXSlider.value = PlayerPrefs.GetFloat("MusicVol");
        }
        else
        {
            SetMusicVol();
        }
        if (PlayerPrefs.HasKey("SFXVol"))
        {
            LoadSFXVolume();
            SFXSlider.value = PlayerPrefs.GetFloat("SFXVol");
        }
        else
        {
            SetSFXVol();
        }
    }

    public void SetMusicVol()
    {

        float volume = musicSlider.value;
        float percent = volume / 1;
        volume = Mathf.Clamp((percent * 100) - 80, -80, 10);

        gameMixer.SetFloat("MusicVol", volume);
        PlayerPrefs.SetFloat("MusicVol", volume);
    }
    
    public void SetSFXVol()
    {
        float volume = SFXSlider.value;
        float percent = volume / 1;
        volume = Mathf.Clamp((percent * 100) - 80,-80,10);
        

        gameMixer.SetFloat("SFXVol", volume);
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
