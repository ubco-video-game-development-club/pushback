using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButton : MonoBehaviour
{
    public void Restart()
    {
        LevelController.instance.RestartGame();
    }
}
