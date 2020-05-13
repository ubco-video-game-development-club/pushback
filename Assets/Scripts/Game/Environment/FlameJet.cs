using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameJet : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        Player player;
        if (col.TryGetComponent<Player>(out player))
        {
            player.Die();
        }

        Enemy enemy;
        if (col.TryGetComponent<Enemy>(out enemy))
        {
            enemy.Die();
        }
    }

    public void SetFlameJetActive(bool isActive)
    {
        GetComponent<Animator>().SetBool("IsActive", isActive);
        GetComponent<BoxCollider2D>().enabled = isActive;
        GetComponent<SpriteRenderer>().enabled = isActive;
    }
}
