using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private int enemiesPerWave = 5;
    [SerializeField] private float spawnDelay = 0.5f;
    private EnemyPool _enemyPool;

    private int _currentWave;
    private bool _isSpawning;

    private void Start()
    {
        _enemyPool = GetComponent<EnemyPool>();
    }

    public void StartSpawning()
    {
        if (!_isSpawning)
            StartCoroutine(SpawnWaves());
    }

    public void StopSpawning()
    {
        StopCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        _isSpawning = true;

        while (true)
        {
            _currentWave++;
            yield return StartCoroutine(SpawnWave(_currentWave));
            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    private IEnumerator SpawnWave(int waveNumber)
    {
        int spawnCount = enemiesPerWave + waveNumber;

        for (int i = 0; i < spawnCount; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemy = _enemyPool.GetEnemy(); // Get from pool
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        enemy.transform.position = spawnPoint.position;
        enemy.transform.rotation = spawnPoint.rotation;

        if (enemy.TryGetComponent<IEnemy>(out var spawnable))
            spawnable.OnSpawn();
    }
}