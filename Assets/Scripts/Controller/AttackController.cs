using System.Collections;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    private IDashable _dash;
    private IAttackable _heavyAttack;
    private IAttackable _rangedAttack;

    private void Awake()
    {
        _dash = GetComponent<IDashable>();
        _heavyAttack = GetComponent<HeavyAttack>();
        _rangedAttack = GetComponent<RangedAttack>();
    }

    private void OnEnable()
    {
        StartCoroutine(WaitForInputManager());
    }

    private void OnDisable()
    {
        if (Managers.Input != null)
        {
            Managers.Input.OnDash -= PerformDash;
            Managers.Input.OnHeavyAttack -= PerformHeavyAttack;
            Managers.Input.OnRangedAttack -= PerformRangedAttack;
        }
    }

    private IEnumerator WaitForInputManager()
    {
        // Wait until InputManager is initialized and ready
        while (!Managers.IsInitialized) yield return null;

        // Now safe to subscribe
        Managers.Input.OnDash += PerformDash;
        Managers.Input.OnHeavyAttack += PerformHeavyAttack;
        Managers.Input.OnRangedAttack += PerformRangedAttack;
    }

    private void PerformDash() => _dash?.ExecuteAttack();  // Dash is now an attack
    private void PerformHeavyAttack() => _heavyAttack?.ExecuteAttack();
    private void PerformRangedAttack() => _rangedAttack?.ExecuteAttack();
}