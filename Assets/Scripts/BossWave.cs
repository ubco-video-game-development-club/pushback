using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWave : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb2D;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
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
            StartCoroutine(FadeAway());
        }
    }

    private IEnumerator FadeAway()
    {
        animator.SetTrigger("Explode");
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
