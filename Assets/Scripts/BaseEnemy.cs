using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class BaseEnemy : MonoBehaviour, IEnemy
{
    protected Transform Target;

    public virtual void Initialize(Transform target)
    {
        this.Target = target;
    }

    public abstract void PerformAction();
}