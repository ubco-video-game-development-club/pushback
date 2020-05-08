using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    private Text textComponent;

    void Awake()
    {
        textComponent = GetComponent<Text>();
    }

    public void SetScore(int score)
    {
        textComponent.text = "Score: " + score.ToString();
    }

    public void SetGameOver(bool won)
    {
        textComponent.color = won ? Color.green : Color.red;
    }

    public void Reset()
    {
        textComponent.text = "Score: 0";
        textComponent.color = Color.white;
    }
}
