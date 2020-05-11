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
    public CanvasGroup winMenu;
    public CanvasGroup loseMenu;
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

    public void Win()
    {
        EnableMenu(winMenu, true);
        SetGameOver(true);
    }

    public void Lose()
    {
        EnableMenu(loseMenu, true);
        SetGameOver(false);
    }

    public void Pause()
    {
        EnableMenu(pauseMenu, true);
    }

    public void Resume()
    {
        EnableMenu(winMenu, false);
        EnableMenu(loseMenu, false);
        EnableMenu(pauseMenu, false);
    }

    public void CloseHUDInstance()
    {
        HUD.instance = null;
        Destroy(gameObject);
    }

    public void CreateFloatyText(Vector3 position, string text)
    {
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

    private void EnableMenu(CanvasGroup menu, bool enabled)
    {
        menu.GetComponent<Animator>().SetBool("IsVisible", enabled);
        menu.interactable = enabled;
        menu.blocksRaycasts = enabled;
    }
}
