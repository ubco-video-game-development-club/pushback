using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Enemy enemyPrefab;
    public bool unlimitedSpawns = false;
    public int spawnCount = 1;
    public float spawnInterval = 1f;

    private bool isActive;
    private int enemyCount;
    private Enemy currentEnemy;

    void Start()
    {
        isActive = true;
        StartCoroutine(SpawnEnemies());
    }

    public void SetSpawnerActive(bool isActive)
    {
        this.isActive = isActive;
    }

    public void DespawnEnemy()
    {
        if (currentEnemy != null)
        {
            currentEnemy.Die();
        }
        ClearCurrentEnemy();
    }

    public void ClearCurrentEnemy()
    {
        currentEnemy = null;
    }

    private IEnumerator SpawnEnemies()
    {
        while (isActive && enemyCount < spawnCount)
        {
            int layers = LayerMask.NameToLayer("Player") & LayerMask.NameToLayer("Enemy");
            bool isBlocked = Physics2D.OverlapBox(transform.position, Vector2.one, 0, layers) != null;
            if (!isBlocked && !currentEnemy)
            {
                Enemy enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
                enemy.BindToSpawner(this);
                currentEnemy = enemy;
                if (!unlimitedSpawns)
                {
                    enemyCount++;
                }
                yield return new WaitForSeconds(spawnInterval);
            }
            else
            {
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
