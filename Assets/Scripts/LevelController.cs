using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public static LevelController instance;

    public string endMessage;
    public ScoreDisplay scoreDisplay;
    public TimeDisplay timeDisplay;
    public MessageDisplay messageDisplay;

    private int totalScore;
    private float gameTimer;
    private bool isTimerActive;

    void Awake()
    {
        // Enforce a singleton pattern for the LevelController object
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        instance = this;
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
            timeDisplay.SetTime(gameTimer);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Main");
        }
    }

    public void AddScore(int score)
    {
        totalScore += score;
        scoreDisplay.SetScore(totalScore);
    }

    public void Win()
    {
        isTimerActive = false;
        scoreDisplay.SetCompleted(true);
        timeDisplay.SetCompleted(true);
        messageDisplay.SetMessage(endMessage);
    }

    public void Lose()
    {
        isTimerActive = false;
        scoreDisplay.SetCompleted(false);
        timeDisplay.SetCompleted(false);
        messageDisplay.SetMessage(endMessage);
    }
}
