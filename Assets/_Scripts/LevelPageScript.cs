using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelPageScript : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField]
    int level;

    [SerializeField]
    GameObject notSelectedGO;
    [SerializeField]
    GameObject selectedGO;
    [SerializeField]
    int defaultLayer;
    public bool selected;

    [SerializeField]
    public RectTransform myRect;

    [SerializeField]
    LevelPageHolder levelPageHolder;

    [SerializeField]
    TMP_Text Title;
    [SerializeField]
    TMP_Text time;

    [SerializeField]
    LoadingScreenScript loadingScreen;

    bool usingCD = false;
    [SerializeField]
    GameObject CDGO;

    GAMEMANAGER gameManager;
    SaveData data;
    private void Awake()
    {
        myRect = GetComponent<RectTransform>();
    }
    private void Start()
    {
        levelPageHolder = GameObject.FindFirstObjectByType<LevelPageHolder>();
        gameManager = GAMEMANAGER.Instance;
        data = gameManager.getSaveData();
        loadingScreen = FindFirstObjectByType<LoadingScreenScript>();
        updateGameInfo();
        selectPage();
    }

    public void play()
    {
        GAMEMANAGER.Instance.SetCD(usingCD);
        loadingScreen.LoadLevel(level + 1);
    }

    public void updateGameInfo()
    {
        float timeData = 0;
        string timeScore = "";
        switch (level)
        {
            default:
                timeScore = "Page is not set to a level";
                break;
            case 1:
                timeData = data.Hi_Score_Level_One;
                break;
            case 2:
                timeData = data.Hi_Score_Level_Two;
                break;
            case 3:
                timeData = data.Hi_Score_Level_Three;
                break;
            case 4:
                timeData = data.Hi_Score_Level_Four;
                break;
            case 5:
                timeData = data.Hi_Score_Level_Five;
                break;
        }

        double minute = Mathf.FloorToInt(timeData / 60);
        double seconds = Math.Truncate((timeData - (60 * minute)));
        double miliseconds = ((Math.Truncate((timeData - (60 * minute)) * 100) / 100) % 1) * 100;
        if (timeData > 10)
        {
            timeScore = string.Format("{0:0}:{1:00}:{2:00}", minute, seconds, miliseconds);
        }
        else
        {
            timeScore = string.Format("0:{0:00}:{1:00}", seconds, miliseconds);
        }

        time.text = "Time: " + timeScore;
        Title.text = "Level " + level;

    }

    private void OnMouseOver()
    {
        Debug.Log("bring to front");
        selectPage();
    }
    public void selectPage()
    {
        levelPageHolder.resetPages();
        myRect.SetAsLastSibling();
        notSelectedGO.SetActive(false);
        selectedGO.SetActive(true);

        selected = true;
    }
    public void deselectPage()
    {
        notSelectedGO.SetActive(true);
        selectedGO.SetActive(false);

        selected = false;
    }

    public void SetCD()
    {
        switch (level)
        {
            default:
                Debug.LogWarning("CD doesnt exist yet for this level, if you see this message please tell emery to add it in");
                break;
            case 1:
                if (data.Level_One_B_Side_Locked == false)
                    usingCD = !usingCD;
                break;
            case 2:
                if (data.Level_Two_B_Side_Locked == false)
                    usingCD = !usingCD;
                break;
            case 3:
                if (data.Level_Three_B_Side_Locked == false)
                    usingCD = !usingCD;
                break;
            case 4:
                if (data.Level_Four_B_Side_Locked == false)
                    usingCD = !usingCD;
                break;
            case 5:
                if (data.Level_Five_B_Side_Locked == false)
                    usingCD = !usingCD;
                break;
        }
        CDGO.SetActive(usingCD);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        selectPage();
        Debug.Log("hover Enter");
    }
}
