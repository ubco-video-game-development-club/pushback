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
    public float deathDelay = 3f;
    public Vector2 floatyTextOffset;

    private int health;
    private Vector3 direction;
    private Player player;
    private bool alive;
    private bool isMovementEnabled;
    private bool isAttackEnabled;
    private bool isBeingPushed;
    private EnemySpawner spawner;
    private Rigidbody2D rb2D;
    private CircleCollider2D circleCollider2D;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        direction = startDirection;
        alive = true;
        isMovementEnabled = true;
        isAttackEnabled = true;
        health = maxHealth;
    }

    void Update()
    {
        if (!alive)
        {
            return;
        }

        Move();
        Attack();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        rb2D.velocity = Vector2.zero;
        
        Boss boss;
        if (col.gameObject.TryGetComponent<Boss>(out boss))
        {
            Die();
            return;
        }

        if (isBeingPushed)
        {
            CancelPush();
        }
    }

    public void Die()
    {
        LevelController.instance.AddScore(killScore);
        spawner.ClearCurrentEnemy();
        animator.SetTrigger("Die");
        alive = false;
        circleCollider2D.enabled = false;
        StartCoroutine(FadeAway());
    }

    public void BindToSpawner(EnemySpawner spawner)
    {
        this.spawner = spawner;
    }

    public void Push(Vector3 direction, int damage, float speed, float distance)
    {
        if (isBeingPushed)
        {
            return;
        }
        
        rb2D.velocity = direction.normalized * speed;
        isBeingPushed = true;
        animator.SetBool("IsBeingPushed", isBeingPushed);
        StartCoroutine(PushOverDistance(transform.position, damage, distance));
    }

    private IEnumerator PushOverDistance(Vector3 start, int damage, float distance)
    {
        float distPushed = 0;
        float backupTimer = 0;
        while (isBeingPushed && distPushed < distance && backupTimer < 5f)
        {
            yield return null;
            distPushed = Vector3.Distance(start, transform.position);
            backupTimer += Time.deltaTime;
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
        animator.SetBool("IsBeingPushed", isBeingPushed);
        StartCoroutine(Stagger());
    }

    private IEnumerator Stagger()
    {
        animator.SetBool("IsStunned", true);
        isMovementEnabled = false;
        isAttackEnabled = false;
        yield return new WaitForSeconds(staggerDuration);
        animator.SetBool("IsStunned", false);
        isMovementEnabled = true;
        isAttackEnabled = true;
    }

    private IEnumerator FadeAway()
    {
        Color fadeColor = spriteRenderer.color;
        float t = 0;
        while (t < deathDelay)
        {
            fadeColor.a = Mathf.Lerp(1f, 0, t / deathDelay);
            spriteRenderer.color = fadeColor;
            t += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }

    private void Damage(int damage)
    {
        Vector3 offset = floatyTextOffset;
        HUD.instance.CreateFloatyText(transform.position + offset, "-" + damage);
        
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Move()
    {
        if (!isMovementEnabled || isBeingPushed)
        {
            return;
        }

        float distToPlayer = Vector2.Distance(transform.position, player.transform.position);
        bool isMoving = distToPlayer <= detectDistance && distToPlayer > attackDistance;

        animator.SetBool("IsMoving", isMoving);

        if (isMoving)
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
            animator.SetTrigger("Attack");
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
