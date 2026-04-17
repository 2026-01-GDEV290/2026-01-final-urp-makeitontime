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
        loadingScreen.LoadLevel(level + 2);
    }

    public void updateGameInfo()
    {
        string timeScore;
        switch (level)
        {
            default:
                timeScore = "Page is not set to a level, please set it to a level in the LevelPageScript";
                break;
            case 1:
                timeScore = data.Hi_Score_Level_One;
                break;
            case 2:
                timeScore = data.Hi_Score_Level_Two;
                break;
            case 3:
                timeScore = data.Hi_Score_Level_Three;
                break;
            case 4:
                timeScore = data.Hi_Score_Level_Four;
                break;
            case 5:
                timeScore = data.Hi_Score_Level_Five;
                break;
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        selectPage();
        Debug.Log("hover Enter");
    }
}
