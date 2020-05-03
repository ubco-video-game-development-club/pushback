using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D()
    {
        Debug.Log("Checkpoint reached!");
        LevelController.instance.EndGame();
    }
}
