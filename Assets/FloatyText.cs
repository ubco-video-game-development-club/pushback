using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatyText : MonoBehaviour
{
    public Text textDisplay;

    public void DisplayText(string text)
    {
        textDisplay.text = text;
        Destroy(gameObject, 3f);
    }
}
