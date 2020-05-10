using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOrb : MonoBehaviour
{
    private bool isReflected;
    private Rigidbody2D rb2D;
    private BoxCollider2D boxCollider2D;

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Player player;
        if (col.gameObject.TryGetComponent<Player>(out player))
        {
            player.Die();
        }

        Shockwave shockwave;
        if (!col.TryGetComponent<Shockwave>(out shockwave))
        {
            rb2D.velocity = Vector2.zero;
            StartCoroutine(Explode());
        }
    }

    public void Reflect()
    {
        rb2D.velocity = -rb2D.velocity;
        isReflected = true;
    }

    public bool IsReflected()
    {
        return isReflected;
    }

    private IEnumerator Explode()
    {
        boxCollider2D.enabled = false;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
