﻿using System.Collections;
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
    }

    public void SetFlameJetActive(bool isActive)
    {
        GetComponent<BoxCollider2D>().enabled = isActive;
        GetComponent<SpriteRenderer>().enabled = isActive;
    }
}
