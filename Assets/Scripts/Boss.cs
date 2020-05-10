using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Phase 1")]
    public List<EnemySpawner> spawners;
    public ForceField forceField;
    public int forceFieldHealth = 5;
    public float forceFieldFadeTime = 1f;
    public float forceFieldRadius = 1.5f;

    [Header("Phase 2")]
    public List<BossCrystal> crystals;
    public int crystalCount = 4;

    [Header("Phase 3")]
    public int bossHealth = 10;
    public float bossRadius = 1.2f;
    public BossWave wavePrefab;
    public float waveSpeed = 1f;
    public float waveInterval = 2f;
    public float waveSpawnOffset = 1f;
    public float deathDelay = 1f;

    private bool isVulnerable;
    private Player player;
    private Animator animator;
    private CircleCollider2D circleCollider2D;

    void Awake()
    {
        animator = GetComponent<Animator>();
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        circleCollider2D.radius = forceFieldRadius;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!isVulnerable)
        {
            Enemy enemy;
            if (col.gameObject.TryGetComponent<Enemy>(out enemy))
            {
                DamageForceField();
            }
        }
    }

    public void BreakCrystal()
    {
        crystalCount--;
        if (crystalCount <= 0)
        {
            isVulnerable = true;
            circleCollider2D.enabled = true;
            circleCollider2D.radius = bossRadius;
            StartCoroutine(WaveAttack());
        }
    }

    public void TakeDamage()
    {
        if (!isVulnerable)
        {
            return;
        }

        animator.SetTrigger("Damage");
        bossHealth--;
        if (bossHealth <= 0)
        {
            StartCoroutine(DeathSequence());
        }
    }

    private void DamageForceField()
    {
        animator.SetTrigger("Damage");
        forceFieldHealth--;
        if (forceFieldHealth <= 0)
        {
            foreach (EnemySpawner spawner in spawners)
            {
                spawner.SetSpawnerActive(false);
                spawner.DespawnEnemy();
            }
            forceField.Break(forceFieldFadeTime);

            foreach (BossCrystal crystal in crystals)
            {
                crystal.ActivateCrystal();
            }
            circleCollider2D.enabled = false;
        }
    }

    private IEnumerator WaveAttack()
    {
        while (isVulnerable)
        {
            animator.SetTrigger("Attack");
            Vector3 dirToPlayer = (player.transform.position - transform.position).normalized;
            Quaternion rotation = Quaternion.FromToRotation(Vector3.left, dirToPlayer);
            Vector3 offset = dirToPlayer * waveSpawnOffset;
            BossWave wave = Instantiate(wavePrefab, transform.position + offset, rotation);
            wave.GetComponent<Rigidbody2D>().velocity = dirToPlayer * waveSpeed;
            yield return new WaitForSeconds(waveInterval);
        }
    }

    private IEnumerator DeathSequence()
    {
        yield return new WaitForSeconds(deathDelay);
        Destroy(gameObject);
    }
}
