using UnityEngine;
using System;

public class InputManager : MonoBehaviour, IGameManager
{
    public ManagerStatus Status {get; private set;}


    public static InputManager Instance { get; private set; }

    public event Action OnDash;
    public event Action OnHeavyAttack;
    public event Action OnRangedAttack;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        
        Debug.Log("InputManager", this);
    }


    public void Startup()
    {
        Debug.Log("Input manager starting...");

        // these values could be initialized with saved data

        // any long-running startup tasks go here, and set status to 'Initializing' until those tasks are complete
        Status = ManagerStatus.Started;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) OnDash?.Invoke();
        if (Input.GetKeyDown(KeyCode.Q)) OnHeavyAttack?.Invoke();
        if (Input.GetKeyDown(KeyCode.E)) OnRangedAttack?.Invoke();
    }
}