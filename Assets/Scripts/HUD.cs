using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public static HUD instance;

    public ScoreDisplay scoreDisplay;
    public TimeDisplay timeDisplay;
    public MessageDisplay messageDisplay;

    void Awake()
    {
        // Enforce a singleton pattern for the HUD object
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

    public void SetTime(float time)
    {
        timeDisplay.SetTime(time);
    }

    public void SetScore(int score)
    {
        scoreDisplay.SetScore(score);
    }

    public void SetGameOver(bool won)
    {
        scoreDisplay.SetGameOver(won);
        timeDisplay.SetGameOver(won);
    }

    public void SetMessage(string message)
    {
        messageDisplay.SetMessage(message);
    }

    public void Reset()
    {
        timeDisplay.Reset();
        scoreDisplay.Reset();
        messageDisplay.Reset();
    }
}
