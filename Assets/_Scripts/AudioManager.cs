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

    public GAMEMANAGER gameManager = null;
    public SaveData data;

    private void Start()
    {
        gameManager = FindFirstObjectByType<GAMEMANAGER>();
        if (gameManager != null)
        {
            data = gameManager.getSaveData();
        }
        else
        {
            Debug.Log("Game Manager == null");
        }

        gameManager.Load_Game();

        if (data.MusicVolume >= 0f && data.MusicVolume <= .75f)
        {
            LoadMusicVolume();
        }
        else
        {
            SetMusicVol();
        }
        if (data.SFXVolume >= 0f && data.SFXVolume <= .75f)
        {
            LoadSFXVolume();
        }
        else
        {
            SetSFXVol();
        }
    }

    public void SetMusicVol()
    {
        float volume = musicSlider.value;

        data.MusicVolume = volume;

        float percent = volume / 1;
        volume = Mathf.Clamp((percent * 100) - 80, -80, 10);

        gameMixer.SetFloat("MusicVol", volume);

        gameManager.Save_Game();
    }
    
    public void SetSFXVol()
    {
        float volume = SFXSlider.value;
        data.SFXVolume = volume;

        float percent = volume / 1;
        volume = Mathf.Clamp((percent * 100) - 80,-80,10);
        

        gameMixer.SetFloat("SFXVol", volume);

        gameManager.Save_Game();
    }
    public void LoadMusicVolume()
    {
        musicSlider.value = data.MusicVolume;
        SetMusicVol();
    }
    public void LoadSFXVolume()
    {
        SFXSlider.value = data.MusicVolume;

        SetSFXVol();
    }
}
