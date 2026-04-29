using System.Collections;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    [SerializeField]
    GameObject gameOverUI;
    [SerializeField]
    Animator anim;
    [SerializeField]
    AudioSource bark;
    [SerializeField]
    AudioSource bars;

    bool gameOver = false;

    private void Awake()
    {
        Time.timeScale = 1;
        gameOverUI.SetActive(false);
        bars.Play();
    }
    public void GameOver()
    {
        if (gameOver == false)
        {
            Time.timeScale = 0f;
            gameOverUI.SetActive(true);
            anim.Play("GameOver");
            StartCoroutine(PlayBark());
            gameOver = true;
        }
    }

    IEnumerator PlayBark()
    {
        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName("PlayBark"));
        bark.Play();
    }
}
