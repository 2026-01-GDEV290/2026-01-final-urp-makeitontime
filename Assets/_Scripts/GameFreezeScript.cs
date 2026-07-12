using UnityEngine;
using UnityEngine.Audio;

public class GameFreezeScript : MonoBehaviour
{
    [SerializeField]
    private AudioMixer gameMixer;

    float SFXVolume;

    bool gameOver = false;
    bool pause = false;
    bool gameWon = false;
    bool dialouge = false;
    bool cutscene = false;

    private void Awake()
    {
        checkIfShouldBePaused();
        gameMixer.GetFloat("SFXVol", out SFXVolume);
    }
    public void pauseGame()
    {
        pause = true;
        checkIfShouldBePaused();
    }
    public void gameLoseEnded()
    {
        gameOver = true;
        checkIfShouldBePaused();
    }

    public void gameWonEnd()
    {
        gameWon = true;
        checkIfShouldBePaused();
    }
    public void dialougeScreen()
    {
        dialouge = true;
        checkIfShouldBePaused();
    }
    public void unpause()
    {
        pause = false;
        checkIfShouldBePaused();
    }
    public void ungameLoseEnded()
    {
        gameOver = false;
        checkIfShouldBePaused();
    }
    public void ungameWonEnd()
    {
        gameWon = false;
        checkIfShouldBePaused();
    }
    public void undialougeScreen()
    {
        dialouge = false;
        checkIfShouldBePaused();
    }
    public void cutsceneScreen()
    {
        cutscene = true;
        checkIfShouldBePaused();
    }
    public void uncutsceneScreen()
    {
        cutscene = false;
        checkIfShouldBePaused();
    }

    private void checkIfShouldBePaused()
    {
        if (dialouge || gameWon || gameOver || pause || cutscene)
        {
            gameMixer.GetFloat("SFXVol", out SFXVolume);
            gameMixer.SetFloat("SFXVol", -80f);
            Time.timeScale = 0f;
        }
        else
        {
            gameMixer.SetFloat("SFXVol", SFXVolume);
            Time.timeScale = 1;
        }
    }
}
