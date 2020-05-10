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
    private bool isEnemySpawned;

    void Start()
    {
        isActive = true;
        StartCoroutine(SpawnEnemies());
    }

    public void SetSpawnerActive(bool isActive)
    {
        this.isActive = isActive;
    }

    public void ClearCurrentEnemy()
    {
        isEnemySpawned = false;
    }

    private IEnumerator SpawnEnemies()
    {
        while (isActive && enemyCount < spawnCount)
        {
            bool isBlocked = Physics2D.OverlapBox(transform.position, Vector2.one, 0, LayerMask.NameToLayer("Main")) != null;
            if (!isBlocked && !isEnemySpawned)
            {
                isEnemySpawned = true;
                Enemy enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
                enemy.BindToSpawner(this);
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
