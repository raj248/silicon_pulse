using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(InputManager))]

public class Managers : MonoBehaviour {
    public static PlayerManager Player {get; private set;}
    public static InputManager Input {get; private set;}
    private List<IGameManager> _startSequence;
    
    public static bool IsInitialized { get; private set; }
	
    void Awake() {
        Player = GetComponent<PlayerManager>();
        Input = GetComponent<InputManager>();

        _startSequence = new List<IGameManager>();
        _startSequence.Add(Player);
        _startSequence.Add(Input);

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