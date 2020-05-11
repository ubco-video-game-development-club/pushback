using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public static HUD instance;

    public ScoreDisplay scoreDisplay;
    public TimeDisplay timeDisplay;
    public MessageDisplay messageDisplay;
    public CanvasGroup pauseMenu;
    public FloatyText floatyTextPrefab;

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

    public void Pause()
    {
        pauseMenu.GetComponent<Animator>().SetBool("IsVisible", true);
        pauseMenu.interactable = true;
    }

    public void Resume()
    {
        pauseMenu.GetComponent<Animator>().SetBool("IsVisible", false);
        pauseMenu.interactable = false;
    }

    public void CloseHUDInstance()
    {
        HUD.instance = null;
        Destroy(gameObject);
    }

    public void CreateFloatyText(Vector3 position, string text)
    {
        Debug.Log(position);
        FloatyText floatyText = Instantiate(floatyTextPrefab, position, Quaternion.identity);
        floatyText.DisplayText(text);
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
