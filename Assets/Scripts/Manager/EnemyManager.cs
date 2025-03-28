using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour, IGameManager
{
    public static EnemyManager Instance { get; private set; }
    public static EnemyPool EnemyPool { get; private set; }
    public static EnemySpawner Spawner { get; private set; }
    public ManagerStatus Status { get; private set; }

    private readonly HashSet<IEnemy> _activeEnemies = new();  // Only active enemies

    [SerializeField] private Transform player;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void Startup()
    {
        EnemyPool = GetComponent<EnemyPool>();
        Spawner = GetComponent<EnemySpawner>();
        Debug.Log("Spawner starting..." + Spawner);
        Status = ManagerStatus.Started;
    }

    public void RegisterEnemy(IEnemy enemy)
    {
        if (enemy==null) Debug.Log("enemy is null");
        enemy.Initialize(player);
        Debug.Log("Initialized with Player");
        _activeEnemies.Add(enemy);
    }

    public void UnregisterEnemy(IEnemy enemy)
    {
        _activeEnemies.Remove(enemy);
    }

    private void Update()
    {
        foreach (var enemy in _activeEnemies)
        {
            enemy.PerformAction();
        }
    }
}