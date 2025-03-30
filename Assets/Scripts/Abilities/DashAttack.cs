using System.Diagnostics.CodeAnalysis;
using Cinemachine;
using UnityEngine;

[SuppressMessage("ReSharper", "CheckNamespace")]
public class DashAttack : MonoBehaviour, IDashable
{
    private Rigidbody _rb;
    public bool IsDashing { get; private set; }
    private Vector3 _forward;

    [SerializeField] private Transform target;
    [SerializeField] private FloatingJoystick fjsLeft;
    [SerializeField] private float dashForce = 20f;
    [SerializeField] private float dashDuration = 0.2f;

    // 🎇 Dash Particle Effects
    [SerializeField] private ParticleSystem dashTrailRingEffect;
    [SerializeField] private float dashOffset;



    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Dash(Vector3 direction, float force, float duration)
    {
        if (IsDashing) return;
        // Get Dash Direction
        _forward = Vector3.Cross(target.right, Vector3.up);
        if (fjsLeft.Horizontal != 0 && fjsLeft.Vertical != 0)
        {
            _forward = (target.right * fjsLeft.Horizontal + _forward * fjsLeft.Vertical).normalized;
        }
        PlayDashEffects();

        // Start Dash
        IsDashing = true;
        _rb.linearVelocity = Vector3.zero;


        _rb.AddForce(_forward.normalized * force, ForceMode.Impulse);
        
        Invoke(nameof(EndDash), duration);
    }

    private void EndDash()
    {
        IsDashing = false;
        _rb.linearVelocity = Vector3.zero;
        
    }

    private void PlayDashEffects()
    {
        if (!dashTrailRingEffect) return;
        dashTrailRingEffect.transform.position = transform.position + dashOffset *  _forward;
        dashTrailRingEffect.transform.rotation = Quaternion.LookRotation(-_forward); // Face opposite to dash
        dashTrailRingEffect.Play();

    }

    // Implements IAttackable (so AttackController can trigger Dash)
    public void ExecuteAttack()
    {
        Dash(transform.forward, dashForce, dashDuration);
    }
}
