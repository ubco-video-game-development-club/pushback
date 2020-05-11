using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    public void LoadMainMenu()
    {
        LevelController.instance.ResumeGame();
        LevelController.instance.CloseGameInstance();
        SceneManager.LoadScene("MainMenu");
    }
}
