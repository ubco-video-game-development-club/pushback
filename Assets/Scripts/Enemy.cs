using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movement")]
    public float movementSpeed = 1f;
    public Vector2 startDirection;
    [Tooltip("Minimum rotation speed towards the player in degrees per second.")]
    public float minRotationSpeed = 10f;
    [Tooltip("Maximum rotation speed towards the player in degrees per second.")]
    public float maxRotationSpeed = 30f;

    [Header("Combat")]
    public int maxHealth = 1;
    public float detectDistance = 3f;
    public float attackDistance = 1f;
    public float attackDelay = 0.5f;
    public float attackCooldown = 1f;
    public float staggerDuration = 0.5f;
    public int killScore = 50;
    public int damageScore = 10;

    private int health;
    private Vector3 direction;
    private Player player;
    private bool isMovementEnabled;
    private bool isAttackEnabled;
    private bool isBeingPushed;
    private Rigidbody2D rb2D;

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        direction = startDirection;
        isMovementEnabled = true;
        isAttackEnabled = true;
        health = maxHealth;
    }

    void Update()
    {
        Move();
        Attack();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (isBeingPushed)
        {
            CancelPush();
        }
    }

    public void Push(Vector3 direction, int damage, float speed, float distance)
    {
        rb2D.velocity = direction.normalized * speed;
        isBeingPushed = true;
        StartCoroutine(PushOverDistance(transform.position, damage, distance));
    }

    private IEnumerator PushOverDistance(Vector3 start, int damage, float distance)
    {
        float distPushed = 0;
        while (isBeingPushed && distPushed < distance)
        {
            yield return null;
            distPushed = Vector3.Distance(start, transform.position);
        }

        if (isBeingPushed)
        {
            CancelPush();
        }
        else
        {
            int distCount = Mathf.CeilToInt(distPushed);
            LevelController.instance.AddScore(damageScore * distCount);

            Damage(damage * distCount);
        }
    }

    private void CancelPush()
    {
        rb2D.velocity = Vector2.zero;
        isBeingPushed = false;
        StartCoroutine(Stagger());
    }

    private IEnumerator Stagger()
    {
        isMovementEnabled = false;
        isAttackEnabled = false;
        yield return new WaitForSeconds(staggerDuration);
        isMovementEnabled = true;
        isAttackEnabled = true;
    }

    private void Damage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        LevelController.instance.AddScore(killScore);
        Destroy(gameObject);
    }

    private void Move()
    {
        if (!isMovementEnabled || isBeingPushed)
        {
            return;
        }

        float distToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distToPlayer <= detectDistance && distToPlayer > attackDistance)
        {
            float radiansDelta = Mathf.Lerp(minRotationSpeed, maxRotationSpeed, distToPlayer / detectDistance) * Mathf.Deg2Rad;
            Vector3 dirToPlayer = (player.transform.position - transform.position).normalized;
            direction = Vector3.RotateTowards(direction, dirToPlayer, radiansDelta, 0);
            transform.position += direction * movementSpeed * Time.deltaTime;
        }
    }

    private void Attack()
    {
        if (!isAttackEnabled || isBeingPushed)
        {
            return;
        }

        float distToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distToPlayer <= attackDistance)
        {
            StartCoroutine(AttackCooldown());
            StartCoroutine(DelayedAttack());
        }
    }

    private IEnumerator AttackCooldown()
    {
        isAttackEnabled = false;
        yield return new WaitForSeconds(attackCooldown);
        isAttackEnabled = true;
    }

    private IEnumerator DelayedAttack()
    {
        isMovementEnabled = false;
        yield return new WaitForSeconds(attackDelay);
        isMovementEnabled = true;

        float distToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distToPlayer <= attackDistance)
        {
            player.Die();
        }
    }
}
