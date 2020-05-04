using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageDisplay : MonoBehaviour
{
    private Text textComponent;

    void Awake()
    {
        textComponent = GetComponent<Text>();
    }

    public void SetMessage(string message)
    {
        textComponent.text = message;
    }
}
