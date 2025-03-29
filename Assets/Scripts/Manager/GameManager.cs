using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour, IGameManager
{  
    public static GameManager Instance;
    public ManagerStatus Status { get; private set; }

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
        Status = ManagerStatus.Started;
    }

    public void StartGame()
    {
        StartCoroutine(WaitForManagers());
        ScoreManager.Instance.StartTracking();
    }

    private IEnumerator WaitForManagers()
    {
        while (!Managers.IsInitialized) { yield return null; }

    }

    public void GameOver()
    {
        ScoreManager.Instance.StopTracking();
        EnemyManager.Spawner.StopSpawning();
        UIManager.Instance.Show("GameOverPanel");
    }
}
