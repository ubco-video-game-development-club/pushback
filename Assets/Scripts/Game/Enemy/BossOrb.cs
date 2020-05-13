using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOrb : MonoBehaviour
{
    private bool isReflected;
    private Animator animator;
    private Rigidbody2D rb2D;
    private BoxCollider2D boxCollider2D;

    void Awake()
    {
        animator = GetComponent<Animator>();
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

    public void Reflect(Vector3 direction)
    {
        float speed = rb2D.velocity.magnitude;
        rb2D.velocity = direction * speed;

        transform.rotation = Quaternion.FromToRotation(Vector3.left, direction);

        gameObject.layer = LayerMask.NameToLayer("PlayerProjectile");

        animator.SetTrigger("Reflect");
        isReflected = true;
    }

    public bool IsReflected()
    {
        return isReflected;
    }

    private IEnumerator Explode()
    {
        animator.SetTrigger("Explode");
        boxCollider2D.enabled = false;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
