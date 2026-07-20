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
    AudioSource DialougeSource;
    AudioSource SFXSource;
    public SaveData data;

    bool loadingAudio;

    private void Start()
    {
        SFXSource = gameObject.AddComponent<AudioSource>();
        SFXSource.playOnAwake = false;
        SFXSource.clip = Resources.Load<AudioClip>("deltarune-explosion 1");
        SFXSource.outputAudioMixerGroup = gameMixer.FindMatchingGroups("SFX")[0];

        DialougeSource = gameObject.AddComponent<AudioSource>();
        DialougeSource.playOnAwake = false;
        DialougeSource.clip = Resources.Load<AudioClip>("S1_1_PRL 1");
        DialougeSource.outputAudioMixerGroup = gameMixer.FindMatchingGroups("Dialouge")[0];

        loadingAudio = true;

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
        loadingAudio = false;
    }

    public void SetDialougeVol()
    {
        float volume = dialougeSlider.value;

        data.DialougeVolume = volume;

        float percent = volume / 1;
        volume = Mathf.Clamp((percent * 100) - 80, -80, 10);

        gameMixer.SetFloat("DialougeVol", volume);


        if(!loadingAudio) DialougeSource.Play();

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

        
        if(!loadingAudio) SFXSource.Play();

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
