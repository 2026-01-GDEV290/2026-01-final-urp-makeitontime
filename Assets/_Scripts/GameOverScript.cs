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
    [SerializeField]
    GameFreezeScript gameFreeze;

    bool gameOver = false;

    private void Awake()
    {
        gameOverUI.SetActive(false);
        gameFreeze = FindFirstObjectByType<GameFreezeScript>();
        gameFreeze.ungameLoseEnded();
    }
    public void GameOver()
    {
        if (gameOver == false)
        {
            gameFreeze.gameLoseEnded();
            gameOverUI.SetActive(true);
            bars.Play();
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
