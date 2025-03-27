using UnityEngine;

public class GameManager : MonoBehaviour
{
    private EnemySpawner _enemySpawner;
    private void Start()
    {
        _enemySpawner = GetComponent<EnemySpawner>();
        _enemySpawner.StartSpawning();
    }
}
