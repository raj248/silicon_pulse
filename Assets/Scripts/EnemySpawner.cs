using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private int enemiesPerWave = 5;
    [SerializeField] private float spawnDelay = 0.5f;

    private int _currentWave;
    private bool _isSpawning;

    public void StartSpawning()
    {
        if (!_isSpawning)
            StartCoroutine(SpawnWaves());
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
        var prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject enemy = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);

        if (enemy.TryGetComponent<ISpawnable>(out var spawnable))
            spawnable.OnSpawn();
    }
}