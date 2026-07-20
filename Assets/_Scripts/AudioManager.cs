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
    [SerializeField]
    Slider dialougeSlider;
    public SaveData data;

    private void Start()
    {

        if (GAMEMANAGER.Instance.getSaveData() != null)
        {
            GAMEMANAGER.Instance.Load_Game();

            data = GAMEMANAGER.Instance.getSaveData();

            LoadMusicVolume();

            LoadSFXVolume();

            LoadDialougeVolume();
        }
        else
        {
            Debug.Log("Game Manager data == null");

            data = new SaveData();

            SetMusicVol();
            SetSFXVol();
            SetDialougeVol();
        }

    }

    public void SetDialougeVol()
    {
        float volume = data.DialougeVolume;

        data.DialougeVolume = volume;

        float percent = volume / 1;
        volume = Mathf.Clamp((percent * 100) - 80, -80, 10);

        gameMixer.SetFloat("DialougeVol", volume);

        GAMEMANAGER.Instance.Save_Game(data);
    }

    public void SetMusicVol()
    {
        float volume = musicSlider.value;

        data.MusicVolume = volume;

        float percent = volume / 1;
        volume = Mathf.Clamp((percent * 100) - 80, -80, 10);

        gameMixer.SetFloat("MusicVol", volume);

        GAMEMANAGER.Instance.Save_Game(data);
    }
    
    public void SetSFXVol()
    {
        float volume = SFXSlider.value;
        data.SFXVolume = volume;

        float percent = volume / 1;
        volume = Mathf.Clamp((percent * 100) - 80,-80,10);
        

        gameMixer.SetFloat("SFXVol", volume);

        GAMEMANAGER.Instance.Save_Game(data);
    }
    public void LoadDialougeVolume()
    {
        dialougeSlider.value = data.DialougeVolume;
        SetDialougeVol();
    }
    public void LoadMusicVolume()
    {
        musicSlider.value = data.MusicVolume;
        SetMusicVol();
    }
    public void LoadSFXVolume()
    {
        SFXSlider.value = data.SFXVolume;
        SetSFXVol();
    }
}
