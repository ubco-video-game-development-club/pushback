using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeDisplay : MonoBehaviour
{
    private Text textComponent;

    void Awake()
    {
        textComponent = GetComponent<Text>();
    }

    public void SetTime(float time)
    {
        textComponent.text = "Time: " + time.ToString("f1") + "s";
    }

    public void SetGameOver(bool won)
    {
        textComponent.color = won ? Color.green : Color.red;
    }

    public void Reset()
    {
        textComponent.text = "Time: 0.0s";
        textComponent.color = Color.white;
    }
}
