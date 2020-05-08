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
    private bool isTimerActive;

    void Awake()
    {
        // Enforce a singleton pattern for the LevelController object
        if (instance == null)
        {
            Debug.Log("Becoming the new instance.");
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("Instance found.");
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
            RestartGame();
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
        isTimerActive = true;
    }

    public void RestartGame()
    {
        totalScore = 0;
        gameTimer = 0;
        HUD.instance.Reset();
        SceneManager.LoadScene("Level1");
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
