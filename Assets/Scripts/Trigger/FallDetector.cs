using UnityEngine;

public class FallDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Handle Player Fall
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.Show("GameOverPanel");
            return;
        }

        // Handle Enemy Fall
        if (other.TryGetComponent<IEnemy>(out var enemy))
        {
            enemy.OnDeath(); // Handle enemy removal (pooled or destroyed)
        }
    }
}