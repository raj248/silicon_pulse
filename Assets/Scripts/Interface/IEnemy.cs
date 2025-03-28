using UnityEngine;

public interface IEnemy
{
    void Initialize(Transform target);
    void PerformAction();
    void OnHit();
    void OnSpawn();
    void OnDeath();
}
