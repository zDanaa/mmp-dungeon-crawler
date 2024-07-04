using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float gameTime;
    public int miniBossSpawnTime;
    public Text timerText;

    public GameObject gameOverUI;
    public GameObject endGameUI;

    void Start()
    {
        StartCoroutine(GameTimer());
    }

    IEnumerator GameTimer()
    {
        float remainingTime = gameTime;
        while (remainingTime > 0)
        {
            if (Mathf.Approximately(remainingTime, miniBossSpawnTime))
            {
                SpawnMiniBoss();
            }

            remainingTime--;

            string minutes = Mathf.FloorToInt(remainingTime / 60F).ToString();
            string seconds = Mathf.FloorToInt(remainingTime % 60).ToString();
            minutes = minutes.Length == 1 ? minutes = "0" + minutes : minutes;
            seconds = seconds.Length == 1 ? seconds = "0" + seconds : seconds;
            timerText.text = minutes + ":" + seconds;

            yield return new WaitForSeconds(1f);
        }
        PlayerWins();
    }

    public void SpawnMiniBoss()
    {
        print("Spawning mini boss");
    }


    public void PlayerWins()
    {
        Time.timeScale = 0f;
        endGameUI.SetActive(true);
        Debug.Log("Player has won the game!");
    }

    public void gameOver()
    {
        Time.timeScale = 0f;
        gameOverUI.SetActive(true);
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}