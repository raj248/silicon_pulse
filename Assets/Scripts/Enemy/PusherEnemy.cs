using UnityEngine;
public class PusherEnemy : BaseEnemy
{
    public float moveSpeed = 3f;
    public float pushForce = 10f;

    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public override void PerformAction()
    {
        if (Target == null) return;

        Vector3 dir = (Target.position - transform.position).normalized;
        _rb.MovePosition(transform.position + dir * (moveSpeed * Time.fixedDeltaTime));
    }

    public override void OnHit()
    {
        throw new System.NotImplementedException();
    }

    public override void OnSpawn()
    {
        EnemyManager.Instance.RegisterEnemy(this);
    }
    public override void OnDeath()
    {
        EnemyManager.EnemyPool.ReturnEnemy(gameObject); // Return to pool instead of destroying
        EnemyManager.Instance.UnregisterEnemy(this);
        ScoreManager.Instance.RegisterEnemyDeath();
        Debug.Log(gameObject.name + " Despawned");
    }


    void OnCollisionEnter(Collision other)
    {
        if (other.transform == Target)
        {
            Vector3 pushDir = (Target.position - transform.position).normalized;
            Rigidbody targetRb = Target.GetComponent<Rigidbody>();
            if (targetRb != null)
            {
                targetRb.AddForce(pushDir * pushForce, ForceMode.Impulse);
            }
        }
    }
}