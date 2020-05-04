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

    public void SetCompleted()
    {
        textComponent.color = Color.green;
    }
}
