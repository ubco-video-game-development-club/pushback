using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public static LevelController instance;

    public string endMessage;

    private int totalScore;
    private float gameTimer;
    private int levelStartScore;
    private float levelStartTime;
    private bool isTimerActive;
    private bool isPaused;

    void Awake()
    {
        // Enforce a singleton pattern for the LevelController object
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        isTimerActive = true;
    }

    void Update()
    {
        if (isTimerActive)
        {
            gameTimer += Time.deltaTime;
            HUD.instance.SetTime(gameTimer);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetLevel();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        levelStartScore = totalScore;
        levelStartTime = gameTimer;
        isTimerActive = true;
        ResumeGame();
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
        HUD.instance.Pause();
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        HUD.instance.Resume();
    }

    public void ResetLevel()
    {
        totalScore = levelStartScore;
        gameTimer = levelStartTime;
        HUD.instance.Reset();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RestartGame()
    {
        totalScore = 0;
        gameTimer = 0;
        HUD.instance.Reset();
        SceneManager.LoadScene("Level1");
    }

    public void CloseGameInstance()
    {
        HUD.instance.CloseHUDInstance();
        LevelController.instance = null;
        Destroy(gameObject);
    }

    public void AddScore(int score)
    {
        totalScore += score;
        HUD.instance.SetScore(totalScore);
    }

    public void Win()
    {
        isTimerActive = false;
        HUD.instance.SetGameOver(true);
        HUD.instance.SetMessage(endMessage);
    }

    public void Lose()
    {
        isTimerActive = false;
        HUD.instance.SetGameOver(false);
        HUD.instance.SetMessage(endMessage);
    }
}
