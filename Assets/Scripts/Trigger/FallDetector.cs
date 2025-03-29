using UnityEngine;

public class FallDetector : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        // Handle Player Fall
        if (other.CompareTag("Player"))
        {
            // GameManager.Instance.GameOver();
            PlayerManager.Instance.OnPlayerDeath();
            return;
        }

        // Handle Enemy Fall
        if (other.TryGetComponent<IEnemy>(out var enemy))
        {
            enemy.OnDeath(); // Handle enemy removal (pooled or destroyed)
        }
    }
}