using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    [SerializeField]
    public LoadingScreenScript loadingScreenScript;
    bool isPaused = false;
    [SerializeField]
    GameObject pauseScreen;
    [SerializeField]
    GameFreezeScript gameFreeze;
    private void Start()
    {
        gameFreeze = FindFirstObjectByType<GameFreezeScript>();
        loadingScreenScript = FindFirstObjectByType<LoadingScreenScript>();
    }
    public void GoToMenu()
    {
        loadingScreenScript.LoadLevel(1);
    }
    public void Continue()
    {
        isPaused = !isPaused;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) )
        {
            if (!isPaused)
            {
                gameFreeze.pauseGame();
                isPaused = true;
            }
            else
            {
                gameFreeze.unpause();
                isPaused = false;
            }
        }
        if (isPaused)
        {
            pauseScreen.SetActive(true);
        }
        else
        {
            pauseScreen.SetActive(false);
        }
    }
}
