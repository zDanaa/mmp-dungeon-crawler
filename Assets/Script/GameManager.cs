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

    void SpawnMiniBoss()
    {
        print("Spawning mini boss");
    }

    void PlayerWins()
    {
        Debug.Log("Player has won the game!");
    }

    public void gameOver(){
        gameOverUI.SetActive(true);
    }
    public void RestartGame(){
        SceneManager.LoadScene("Game");
    }

    public void MainMenu(){
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame(){
        Application.Quit();
    }
}