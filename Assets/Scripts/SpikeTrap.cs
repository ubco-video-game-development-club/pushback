using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public float activationDelay = 1f;

    private BoxCollider2D boxCollider2D;

    void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        StartCoroutine(ActivateSpikes());
    }

    private IEnumerator ActivateSpikes()
    {
        yield return new WaitForSeconds(activationDelay);

        Collider2D[] cols = Physics2D.OverlapBoxAll(transform.position, boxCollider2D.size, 0);
        foreach (Collider2D col in cols)
        {
            Player player;
            if (col.TryGetComponent<Player>(out player))
            {
                player.Die();
            }
        }
    }
}
