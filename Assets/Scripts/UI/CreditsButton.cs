using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsButton : MonoBehaviour
{
    public Animator creditsPanel;

    public void ToggleCredits()
    {
        bool isVisible = creditsPanel.GetBool("IsVisible");
        creditsPanel.SetBool("IsVisible", !isVisible);
    }
}
