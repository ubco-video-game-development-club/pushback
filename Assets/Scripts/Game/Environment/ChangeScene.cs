using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string targetScene;

    void OnTriggerEnter2D(Collider2D col)
    {
        Player player;
        if (col.TryGetComponent<Player>(out player))
        {
            SceneManager.LoadScene(targetScene);
        }
    }
}
