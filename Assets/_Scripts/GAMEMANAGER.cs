using UnityEngine;

public class GAMEMANAGER : MonoBehaviour
{
    private SaveData saveData;

    public static GAMEMANAGER Instance;

    private string filePath;

    bool usingCD = false;

    public float levelBeatTime;
    public float CopsCalled;
    public float CarsHit;

    public float levelPar;

    //Create Singleton
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        
        //get persitentDataPath which is never deleted even when game is deleted
        filePath = Application.persistentDataPath + "/saveData.json";
        Debug.Log(filePath);
        Load_Game();
    }
    public SaveData getSaveData()
    {
        return saveData;
    }
    public void Load_Game()
    {
        if (System.IO.File.Exists(filePath))
        {
            string saveDataJSON = System.IO.File.ReadAllText(filePath);
            saveData = JsonUtility.FromJson<SaveData>(saveDataJSON);
            Debug.Log("Loaded save file: " + filePath);
        }
        else
        {
            Debug.Log("No save file found, creating new data");

            saveData = new SaveData();
            Save_Game();
        }
    }

    public void Save_Game()
    {
        if (saveData == null)
        {
            Debug.LogWarning("saveData was null, creating new SaveData");
            saveData = new SaveData();
        }

        string saveDataJSON = JsonUtility.ToJson(saveData, true);
        System.IO.File.WriteAllText(filePath, saveDataJSON);

        Debug.Log("Game is saved");
    }
    public void Save_Game(SaveData foreignSaveData)
    {
        string saveDataJSON = JsonUtility.ToJson(foreignSaveData);
        Debug.Log(filePath);
        System.IO.File.WriteAllText(filePath, saveDataJSON);
        Debug.Log("Game is saved at: " + filePath);
    }
    public void resetSaveData()
    {
        saveData = new SaveData();
        Save_Game();
    }
    public void SetCD(bool value)
    {
        usingCD = value;
    }
    public bool getUseCD()
    {
        return usingCD;
    }
}
[System.Serializable]
public class SaveData
{
    public float Hi_Score_Level_One = 0;
    public float Hi_Score_Level_Two = 0;
    public float Hi_Score_Level_Three = 0;
    public float Hi_Score_Level_Four = 0;
    public float Hi_Score_Level_Five = 0;

    public bool Level_One_Locked = false;
    public bool Level_Two_Locked = false;
    public bool Level_Three_Locked = false;
    public bool Level_Four_Locked = false;
    public bool Level_Five_Locked = false;

    public bool Level_One_B_Side_Locked = true;
    public bool Level_Two_B_Side_Locked = true;
    public bool Level_Three_B_Side_Locked = true;
    public bool Level_Four_B_Side_Locked = true;
    public bool Level_Five_B_Side_Locked = true;

    public float SFXVolume = 0.5f;
    public float MusicVolume = 0.5f;
    public float DialougeVolume = 0.5f;
    public string valueThree;
}
