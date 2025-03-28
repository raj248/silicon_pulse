using UnityEngine;

public class RangedAttack : MonoBehaviour, IAttackable
{
    public void ExecuteAttack()
    {
        Debug.Log("Ranged Attack Executed!");
    }
}