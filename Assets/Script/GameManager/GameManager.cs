using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Mathematics;

public class GameManager : MonoBehaviour
{
    public float gameTime;
    public int miniBossSpawnTime;
    public Text timerText;
    public GameObject gameOverUI;
    public GameObject endGameUI;
    public GameObject boss;
    private PlayerController player;
    private AlhoonBossController bossInstance;
    private SpawnerScript spawner;
    private bool hasStartedSpawning = false;
    private bool hasSpawnedMiniBoss = false;


    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        StartCoroutine(GameTimer());
    }
    private void Update()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerController>();
        }
        if (hasSpawnedMiniBoss && bossInstance.currentHealth <= 0)
        {
            PlayerWins();
        }
    }

    IEnumerator GameTimer()
    {
        float remainingTime = gameTime;
        while (remainingTime > 0)
        {
            if (!hasStartedSpawning && remainingTime <= gameTime - 10f){
                StartSpawningProcess();
            }

            if (remainingTime % 20 == 0 && remainingTime != gameTime)
            {
                DecreaseRateOfSpawning();
            }

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
    }

    public void StartSpawningProcess()
    {
        spawner = FindObjectOfType<SpawnerScript>();
        spawner.StartSpawning();
        hasStartedSpawning = true;
    }

    public void DecreaseRateOfSpawning(){
        spawner.DecreaseSpawnRate();
    }

    public void SpawnMiniBoss()
    {
        hasSpawnedMiniBoss = true;
        Instantiate(boss, player.transform.position, Quaternion.identity);
        bossInstance = FindObjectOfType<AlhoonBossController>();
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
        AudioManager.Instance.PlayMusic("GameMusic");
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameMenu");
    }

    public void MainMenu()
    {
        AudioManager.Instance.StopMusic("GameMusic");
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}