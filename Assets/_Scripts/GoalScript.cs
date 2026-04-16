using System;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GoalScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    GameObject resultScreen;
    [SerializeField]
    TMP_Text timerText;

    float time;
    private void Start()
    {
        time = 0;
        resultScreen.SetActive(false);
    }
    private void Update()
    {
        debugTime();
        if (Time.timeScale == 1)
        {
            time += Time.deltaTime;
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
        resultScreen.SetActive(true);
        double minute = Mathf.FloorToInt(time / 60);
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
        Time.timeScale = 0;
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
