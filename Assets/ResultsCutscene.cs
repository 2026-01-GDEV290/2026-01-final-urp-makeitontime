using System;
using System.Collections;
using System.Diagnostics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ResultsCutscene : MonoBehaviour
{
    Animator animator;
    GAMEMANAGER gameManager;

    TMP_Text CarsHitTxt;
    TMP_Text CopsCalledTxt;
    TMP_Text TimeTxt;
    TMP_Text RankTxt;
    int level;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(StartCutscene());
    }
    private void Start()
    {
        gameManager = GAMEMANAGER.Instance;        
    }
    
    private void SetInformation()
    {
        CarsHitTxt.text = "Cars hit: " + gameManager.CarsHit;
        CopsCalledTxt.text = "Cops called: " + gameManager.CopsCalled;
        
        double minute = Mathf.FloorToInt(gameManager.levelBeatTime / 60);
        double seconds = Math.Truncate((gameManager.levelBeatTime - (60 * minute)));
        double miliseconds = ((Math.Truncate((gameManager.levelBeatTime - (60 * minute)) * 100) / 100) % 1) * 100;
        if (gameManager.levelBeatTime > 10)
        {
            TimeTxt.text = string.Format("{0:0}:{1:00}:{2:00}", minute, seconds, miliseconds);
        }
        else
        {
            TimeTxt.text = string.Format("0:{0:00}:{1:00}", seconds, miliseconds);
        }

        float grade = Mathf.Ceil((gameManager.levelPar/gameManager.levelBeatTime)* 100) + (gameManager.CarsHit * 10);


        if(grade > 200)
        {
            RankTxt.text = "S";
        }
        else if(grade <= 200 || grade > 150)
        {
            RankTxt.text = "A";
        }
        else if(grade <= 150 || grade > 100)
        {
            RankTxt.text = "B";
        }
        else if(grade <= 100 || grade > 50)
        {
            RankTxt.text = "C";
        }
        else if(grade > 0 || grade <= 50)
        {
            RankTxt.text = "D";
        }
        else if(grade <= 0)
        {
            RankTxt.text = "F";
        }
    }

    IEnumerator StartCutscene()
    {
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("Start");
    }
}
