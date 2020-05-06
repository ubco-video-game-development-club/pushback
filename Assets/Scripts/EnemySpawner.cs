using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Enemy enemyPrefab;
    public float spawnInterval = 1f;

    private bool isActive;

    void Start()
    {
        isActive = true;
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (isActive)
        {
            bool isBlocked = Physics2D.OverlapBox(transform.position, Vector2.one, 0) != null;
            if (!isBlocked)
            {
                Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
