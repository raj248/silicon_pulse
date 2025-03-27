using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private readonly List<IEnemy> _enemies = new List<IEnemy>();
    [SerializeField] private Transform player;

    public void RegisterEnemy(IEnemy enemy)
    {
        enemy.Initialize(player);
        _enemies.Add(enemy);
    }

    void Update()
    {
        foreach (var enemy in _enemies)
        {
            enemy.PerformAction();
        }
    }
}