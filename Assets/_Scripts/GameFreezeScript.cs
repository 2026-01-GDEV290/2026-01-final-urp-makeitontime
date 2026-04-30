using UnityEngine;

public class GameFreezeScript : MonoBehaviour
{
    bool gameOver = false;
    bool pause = false;
    bool gameWon = false;
    bool dialouge = false;
   public void pauseGame()
    {
        pause = true;
        checkIfShouldBePaused();
    }
    public void gameLoseEnded()
    {
        gameOver = true;
        checkIfShouldBePaused();
    }

    public void gameWonEnd()
    {
        gameWon = true;
        checkIfShouldBePaused();
    }
    public void dialougeScreen()
    {
        dialouge = true;
        checkIfShouldBePaused();
    }
    public void unpause()
    {
        pause = false;
        checkIfShouldBePaused();
    }
    public void ungameLoseEnded()
    {
        gameOver = false;
        checkIfShouldBePaused();
    }
    public void ungameWonEnd()
    {
        gameWon = false;
        checkIfShouldBePaused();
    }
    public void undialougeScreen()
    {
        dialouge = false;
        checkIfShouldBePaused();
    }

    private void checkIfShouldBePaused()
    {
        if (dialouge || gameWon || gameOver || pause)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
