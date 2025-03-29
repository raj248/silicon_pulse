using UnityEngine;

public class PlayerManager : MonoBehaviour, IGameManager {
    public ManagerStatus Status {get; private set;}
    public static PlayerManager Instance { get; private set; }


    public int Health {get; private set;}
    public int MaxHealth {get; private set;}
    
    [SerializeField] private int maxLives = 3;
    [SerializeField] private Transform respawnPoint;

    private int _currentLives;
    private Transform _playerTransform;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void Startup() {
        Debug.Log("Player manager starting...");

        // these values could be initialized with saved data
        Health = 50;
        MaxHealth = 100;

        // any long-running startup tasks go here, and set status to 'Initializing' until those tasks are complete
        Status = ManagerStatus.Started;
    }


    private void Start()
    {
        _currentLives = maxLives;
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void ChangeHealth(int value) {
        Health += value;
        if (Health > MaxHealth) {
            Health = MaxHealth;
        } else if (Health < 0) {
            Health = 0;
        }
        Debug.Log($"Health: {Health}/{MaxHealth}");
    }

    public void OnPlayerDeath()
    {
        _currentLives--;

        if (_currentLives > 0)
        {
            RespawnPlayer();
        }
        else
        {
            GameManager.Instance.GameOver();
        }
    }

    private void RespawnPlayer()
    {
        Debug.Log("Respawning Player...");
        _playerTransform.position = respawnPoint.position;
    }

    public int GetLives() => _currentLives;
}