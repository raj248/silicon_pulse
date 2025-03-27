using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public class SpawnItem
    {
        public GameObject prefab;
        public int count;
        public float spawnDelay = 0.5f;
    }

    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private List<SpawnItem> itemsToSpawn;

    public void StartSpawning()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        foreach (var item in itemsToSpawn)
        {
            for (int i = 0; i < item.count; i++)
            {
                Transform point = spawnPoints[Random.Range(0, spawnPoints.Count)];
                GameObject obj = Instantiate(item.prefab, point.position, Quaternion.identity);

                ISpawnable spawnable = obj.GetComponent<ISpawnable>();
                spawnable?.OnSpawn();

                yield return new WaitForSeconds(item.spawnDelay);
            }
        }
    }
}