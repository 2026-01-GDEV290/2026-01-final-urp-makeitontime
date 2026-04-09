using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    [SerializeField]
    public LoadingScreenScript loadingScreenScript;
    bool isPaused = false;
    [SerializeField]
    GameObject pauseScreen;
    private void Start()
    {
        loadingScreenScript = FindFirstObjectByType<LoadingScreenScript>();
    }
    public void GoToMenu()
    {
        loadingScreenScript.LoadLevel(0);
    }
    public void Continue()
    {
        isPaused = !isPaused;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) )
        {
            if (Time.timeScale != 0f && !isPaused)
            {
                isPaused = true;
            }
            else
            {
                isPaused = false;
            }
        }
        if (isPaused)
        {
            Time.timeScale = 0f;
            pauseScreen.SetActive(true);
        }
        else
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }
}
