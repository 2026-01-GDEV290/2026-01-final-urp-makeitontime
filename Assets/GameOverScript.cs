using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    [SerializeField]
    GameObject gameOverUI;

    private void Awake()
    {
        Time.timeScale = 1;
        gameOverUI.SetActive(false);
    }
    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOverUI.SetActive(true);
    }
}
