using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public static LevelController instance;

    public string endMessage;

    public TimeDisplay timeDisplay;
    public MessageDisplay messageDisplay;

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

    public void EndGame()
    {
        isTimerActive = false;
        timeDisplay.SetCompleted();
        messageDisplay.SetMessage(endMessage);
    }
}
