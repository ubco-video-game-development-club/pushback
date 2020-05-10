using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWave : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Player player;
        if (col.gameObject.TryGetComponent<Player>(out player))
        {
            player.Die();
        }

        Destroy(gameObject);
    }
}
