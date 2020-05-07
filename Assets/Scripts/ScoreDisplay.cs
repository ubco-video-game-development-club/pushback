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

    public void SetCompleted(bool won)
    {
        textComponent.color = won ? Color.green : Color.red;
    }
}
