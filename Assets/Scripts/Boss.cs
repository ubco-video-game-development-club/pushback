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

    [Header("Phase 2")]
    public List<BossCrystal> crystals;
    public int crystalCount = 4;

    [Header("Phase 3")]
    public int bossHealth = 10;
    public BossWave wavePrefab;
    public float waveSpeed = 1f;
    public float waveInterval = 2f;
    public float waveSpawnOffset = 1f;
    public float deathDelay = 1f;

    private bool isVulnerable;
    private Player player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
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
        else
        {
            Shockwave shockwave;
            if (col.gameObject.TryGetComponent<Shockwave>(out shockwave))
            {
                TakeDamage();
            }
        }
    }

    public void BreakCrystal()
    {
        crystalCount--;
        if (crystalCount <= 0)
        {
            isVulnerable = true;
        }
    }

    private void DamageForceField()
    {
        forceFieldHealth--;
        if (forceFieldHealth <= 0)
        {
            foreach (EnemySpawner spawner in spawners)
            {
                spawner.SetSpawnerActive(false);
            }
            forceField.Break(forceFieldFadeTime);

            foreach (BossCrystal crystal in crystals)
            {
                crystal.ActivateCrystal();
            }
        }
    }

    private void TakeDamage()
    {
        bossHealth--;
        if (bossHealth <= 0)
        {
            StartCoroutine(DeathSequence());
        }
    }

    private IEnumerator WaveAttack()
    {
        while (isVulnerable)
        {
            Vector3 dirToPlayer = player.transform.position - transform.position;
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
