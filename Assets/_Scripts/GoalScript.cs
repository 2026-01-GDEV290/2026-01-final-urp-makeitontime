using System;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GoalScript : MonoBehaviour
{
    [SerializeField]
    LoadingScreenScript loadingScreen;
    [SerializeField]
    float[] levelPars;
    bool gameWonBool = false;

    [SerializeField]
    GAMEMANAGER gameManager;
    [SerializeField]
    GameFreezeScript gameFreeze;

    float time;
    private void Start()
    {
        time = 0;
        loadingScreen = FindFirstObjectByType<LoadingScreenScript>();
        gameManager = GAMEMANAGER.Instance;
        gameFreeze = FindFirstObjectByType<GameFreezeScript>();
    }
    private void Update()
    {
        //debugTime();
        if (Time.timeScale == 1)
        {
            time += Time.deltaTime;
        }
        if (gameWonBool == true)
        {
            gameFreeze.gameWonEnd();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision at all");
        if (collision.gameObject.layer == 8)
        {
            Debug.Log("Collisin with player");

            gameWon();
        }
    }

    private void gameWon()
    {
        gameWonBool = true;

        /*double minute = Mathf.FloorToInt(time / 60);
        double seconds = Math.Truncate((time - (60 * minute)));
        double miliseconds = ((Math.Truncate((time - (60 * minute)) * 100) / 100) % 1) * 100;
        if (time > 10)
        {
            timerText.text = string.Format("{0:0}:{1:00}:{2:00}", minute, seconds, miliseconds);
        }
        else
        {
            timerText.text = string.Format("0:{0:00}:{1:00}", seconds, miliseconds);
        }
        */

        gameManager.Load_Game();

        SaveData data = gameManager.getSaveData();

        switch (SceneManager.GetActiveScene().buildIndex)
        {
            //Level One Index == 2
            case 2:
                if(data.Hi_Score_Level_One > time || data.Hi_Score_Level_One == 0)
                    data.Hi_Score_Level_One = time;
                    GAMEMANAGER.Instance.levelPar = levelPars[0];
                break;
            case 3:
                if (data.Hi_Score_Level_Two > time || data.Hi_Score_Level_Two == 0)
                    data.Hi_Score_Level_Two = time;
                    GAMEMANAGER.Instance.levelPar = levelPars[1];
                break;
            case 4:
                if (data.Hi_Score_Level_Three > time || data.Hi_Score_Level_Three == 0)
                    data.Hi_Score_Level_Three = time;
                    GAMEMANAGER.Instance.levelPar = levelPars[2];
                break;
            case 5:
                if (data.Hi_Score_Level_Four > time || data.Hi_Score_Level_Four == 0)
                    data.Hi_Score_Level_Four = time;
                    GAMEMANAGER.Instance.levelPar = levelPars[3];
                break;
            case 6:
                if (data.Hi_Score_Level_Five > time || data.Hi_Score_Level_Five == 0)
                    data.Hi_Score_Level_Five = time;
                    GAMEMANAGER.Instance.levelPar = levelPars[4];
                break;
        }
        gameManager.Save_Game(data);

        gameManager.levelBeatTime = time;

        gameManager.levelIndex = SceneManager.GetActiveScene().buildIndex;

        loadingScreen.LoadLevel(5);
    }

    public void retryLevel()
    {
        loadingScreen.LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }
    public void returnToMenu()
    {
        loadingScreen.LoadLevel(1);
    }
    private void debugTime()
    {
        double minute = Mathf.FloorToInt(time / 60);
        double seconds = Math.Truncate((time - (60 * minute)));
        double miliseconds = ((Math.Truncate((time - (60 * minute)) * 100) / 100) % 1) * 100;
        if (time > 10)
        {
            Debug.Log(string.Format("{0:0}:{1:00}:{2:00}", minute, seconds, miliseconds));
        }
        else
        {
            Debug.Log(string.Format("0:{0:00}:{1:00}", seconds, miliseconds));
        }
    }
}
