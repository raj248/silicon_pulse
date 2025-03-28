using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class BaseEnemy : MonoBehaviour, IEnemy
{
    protected Transform Target;

    public virtual void Initialize(Transform target)
    {
        Target = target;
    }

    public abstract void PerformAction();
    public abstract void OnHit();
    public abstract void OnSpawn();
    public abstract void OnDeath();
}