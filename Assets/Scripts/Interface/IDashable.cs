using UnityEngine;

public interface IDashable : IAttackable
{
    void Dash(Vector3 direction, float force, float duration);
}
