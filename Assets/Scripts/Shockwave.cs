using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    private Vector3 origin;
    private int damage;
    private float speed;
    private float distance;
    private Rigidbody2D rb2D;

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
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
            transform.localScale = new Vector3(0.5f, size, 1);
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
    }
}
