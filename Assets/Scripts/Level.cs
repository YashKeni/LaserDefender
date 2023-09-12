using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{

    [SerializeField] float delayInSeconds = 2f;
    public int level;

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Level Selector");
        GameSession gameSession = FindObjectOfType<GameSession>();
        if (gameSession != null)
        {
            gameSession.ResetGame();
        }
        //FindObjectOfType<GameSession>().ResetGame();
    }

    public void LoadGameOver()
    {
        StartCoroutine(GameOverLoad());
    }

    IEnumerator GameOverLoad()
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene("Game Over");
    }

    public void LoadNextLevel()
    {
        StartCoroutine(WaitNLoad());
    }
    IEnumerator WaitNLoad()
    {
        yield return new WaitForSeconds(delayInSeconds);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
        // SceneManager.LoadScene("Game Over");
    }

    public void LoadLevels()
    {
        SceneManager.LoadScene("Level " + level.ToString());
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
