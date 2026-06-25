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
    [SerializeField]
    AudioClip gameOverMusic;

    [SerializeField]
    AudioSource audioSource;

    bool gameOver = false;

    private void Awake()
    {
        gameOverUI.SetActive(false);
        gameFreeze = FindFirstObjectByType<GameFreezeScript>();
        gameFreeze.ungameLoseEnded();

        audioSource = GameObject.FindGameObjectWithTag("mainMusic").GetComponent<AudioSource>();
    }
    public void GameOver()
    {
        if (gameOver == false)
        {
            gameFreeze.gameLoseEnded();

            audioSource.clip = gameOverMusic;
            audioSource.Play();

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
