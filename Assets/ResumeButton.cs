﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeButton : MonoBehaviour
{
    public void Resume()
    {
        LevelController.instance.ResumeGame();
    }
}
