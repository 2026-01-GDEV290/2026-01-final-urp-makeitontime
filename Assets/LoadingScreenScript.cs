using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreenScript : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private TMP_Text loadingText;

    private void Awake()
    {
        loadingScreen.SetActive(false);
    }
    public void LoadLevel(int levelToLoad)
    {
        loadingScreen.SetActive(true);
        StartCoroutine(LoadLevelAsync(levelToLoad));
    }
    public void quitGame()
    {
        Application.Quit();
    }
    IEnumerator LoadLevelAsync(int levelToLoad)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);

        while (!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadingText.text = Mathf.Round(progressValue*100) + "%";
            yield return null;
        }

        Time.timeScale = 1f;
    }
}
