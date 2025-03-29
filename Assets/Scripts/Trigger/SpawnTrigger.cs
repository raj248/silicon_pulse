using System.Collections;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    [SerializeField] private Vector3 rotationAxis = new Vector3(1, 1, 1);
    [SerializeField] private float rotationSpeed = 50f;

    void Update()
    {
        transform.Rotate(rotationAxis * (rotationSpeed * Time.deltaTime), Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(StartSpawn());
    }

    private IEnumerator StartSpawn()
    {
        while (!Managers.IsInitialized) yield return null;
        GameManager.Instance.StartGame();
        EnemyManager.Spawner.StartSpawning();
        gameObject.SetActive(false);
    }
}
