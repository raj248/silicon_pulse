using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int poolSize = 10;

    private readonly Queue<GameObject> _enemyPool = new Queue<GameObject>();

    private void Awake()
    {
        // Pre-instantiate enemies and store them in the pool
        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.SetActive(false);
            _enemyPool.Enqueue(enemy);
        }
    }

    public GameObject GetEnemy()
    {
        if (_enemyPool.Count > 0)
        {
            GameObject enemy = _enemyPool.Dequeue();
            enemy.SetActive(true);
            return enemy;
        }
        else
        {
            // If pool is empty, create a new enemy (optional)
            GameObject enemy = Instantiate(enemyPrefab);
            return enemy;
        }
    }

    public void ReturnEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
        _enemyPool.Enqueue(enemy);
    }
}