using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    private Vector3 origin;
    private int damage;
    private float speed;
    private float distance;
    private BoxCollider2D boxCollider2D;
    private Rigidbody2D rb2D;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ActivateShockwave(Vector3 origin, Vector3 direction, int damage, float speed, float minSize, float maxSize, float distance)
    {
        this.origin = origin;
        this.damage = damage;
        this.speed = speed;
        this.distance = distance;

        rb2D.velocity = direction.normalized * speed;
        StartCoroutine(GrowOverDistance(minSize, maxSize, distance));
    }

    private IEnumerator GrowOverDistance(float minSize, float maxSize, float distance)
    {
        float size = minSize;
        while (size < maxSize)
        {
            float distTravelled = Vector3.Distance(transform.position, origin);
            size = Mathf.Lerp(minSize, maxSize, distTravelled / distance);
            spriteRenderer.size = new Vector2(spriteRenderer.size.x, size);
            boxCollider2D.size = new Vector2(boxCollider2D.size.x, size);
            yield return null;
        }
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Enemy enemy;
        if (col.gameObject.TryGetComponent<Enemy>(out enemy))
        {
            Vector3 direction = enemy.transform.position - origin;
            float remainingDist = distance - Vector3.Distance(origin, enemy.transform.position);
            enemy.Push(direction, damage, speed, remainingDist);
        }

        BossOrb orb;
        if (col.gameObject.TryGetComponent<BossOrb>(out orb))
        {
            Vector3 direction = (orb.transform.position - origin).normalized;
            orb.Reflect(direction);
        }

        Boss boss;
        if (col.gameObject.TryGetComponent<Boss>(out boss))
        {
            boss.TakeDamage();
        }
    }
}
