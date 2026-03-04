using UnityEngine;

public class statsTest : MonoBehaviour
{
    public SaveData data;
    GAMEMANAGER gameManager;

    private void Start()
    {
        gameManager = FindFirstObjectByType<GAMEMANAGER>().GetComponent<GAMEMANAGER>();
        if (gameManager != null)
        {
            data = gameManager.getSaveData();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            gameManager.Save_Game(data);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            gameManager.Load_Game();
            data = gameManager.getSaveData();
        }
    }
}
