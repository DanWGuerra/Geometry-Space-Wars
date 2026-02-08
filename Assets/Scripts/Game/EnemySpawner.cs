using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs;

    
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float minimumSpawnInterval = 0.4f;
    [SerializeField] private float spawnAcceleration = 0.05f;

    
    [SerializeField] private Collider2D screenBounds;

    
    [SerializeField] private PlayerHealth playerHealth;

    private float spawnTimer;
    private bool canSpawn = true;

    private void OnEnable()
    {
        if (playerHealth != null)
            playerHealth.OnPlayerDied += StopSpawning;
    }

    private void OnDisable()
    {
        if (playerHealth != null)
            playerHealth.OnPlayerDied -= StopSpawning;
    }

    private void Update()
    {
        if (!canSpawn)
            return;

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnEnemy();
            spawnTimer = 0f;
            IncreaseDifficulty();
        }
    }

    private void StopSpawning()
    {
        canSpawn = false;
    }

    private void SpawnEnemy()
    {
        if (enemyPrefabs.Count == 0)
            return;

        GameObject enemyPrefab = GetRandomEnemy();
        Vector2 spawnPosition = GetRandomTopPosition();

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    private GameObject GetRandomEnemy()
    {
        int index = Random.Range(0, enemyPrefabs.Count);
        return enemyPrefabs[index];
    }

    private Vector2 GetRandomTopPosition()
    {
        Bounds bounds = screenBounds.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = bounds.max.y;

        return new Vector2(x, y);
    }

    private void IncreaseDifficulty()
    {
        spawnInterval = Mathf.Max(
            minimumSpawnInterval,
            spawnInterval - spawnAcceleration
        );
    }
}
