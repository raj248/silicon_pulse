using UnityEngine;

public class ScoreManager : MonoBehaviour, IGameManager
{
    public static ScoreManager Instance { get; private set; }
    public ManagerStatus Status { get; private set; }

    public float TimeSurvived { get; private set; }
    public int EnemiesDefeated { get; private set; }

    private bool _isTracking;

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
        Debug.Log("Score Manager starting...");
        Status = ManagerStatus.Started;
    }

    public void StartTracking()
    {
        TimeSurvived = 0f;
        EnemiesDefeated = 0;
        _isTracking = true;
    }

    private void Update()
    {
        if (_isTracking)
        {
            TimeSurvived += Time.deltaTime;
        }
    }

    public void RegisterEnemyDeath()
    {
        EnemiesDefeated++;
    }

    public void StopTracking()
    {
        _isTracking = false;
    }
}
