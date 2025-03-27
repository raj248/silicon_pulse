using UnityEngine;

public class HeavyAttack : MonoBehaviour, IAttackable
{
    public void ExecuteAttack()
    {
        Debug.Log("Heavy Attack Executed!");
    }
}