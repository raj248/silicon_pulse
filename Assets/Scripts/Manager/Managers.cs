using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(InputManager))]

public class Managers : MonoBehaviour {
    public static PlayerManager Player {get; set;}
    public static InputManager Input {get; set;}
    public static GameManager Game {get; set;}
    public static EnemyManager Enemy  {get; set;}
    public static UIManager UI {get; set;}
    
    private List<IGameManager> _startSequence;
    
    public static bool IsInitialized { get; private set; }
	
    void Awake() {
        Player = GetComponent<PlayerManager>();
        Input = GetComponent<InputManager>();
        Game = FindAnyObjectByType<GameManager>();
        Enemy = FindAnyObjectByType<EnemyManager>();
        UI = FindAnyObjectByType<UIManager>();

        _startSequence = new List<IGameManager>();
        _startSequence.Add(Player);
        _startSequence.Add(Input);
        _startSequence.Add(Game);
        _startSequence.Add(Enemy);
        _startSequence.Add(UI);

        StartCoroutine(StartupManagers());
    }

    private IEnumerator StartupManagers() {
        foreach (IGameManager manager in _startSequence) {
            manager.Startup();
        }

        yield return null;

        int numModules = _startSequence.Count;
        int numReady = 0;

        while (numReady < numModules) {
            int lastReady = numReady;
            numReady = 0;

            foreach (IGameManager manager in _startSequence) {
                if (manager.Status == ManagerStatus.Started) {
                    numReady++;
                }
            }

            if (numReady > lastReady)
                Debug.Log($"Progress: {numReady}/{numModules}");
			
            yield return null;
        }
		
        Debug.Log("All managers started up");
        IsInitialized = true;
    }
}