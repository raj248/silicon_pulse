using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RelativeMovement : MonoBehaviour
{
    [SerializeField] Transform target;

    public float moveSpeed = 6.0f;
    public float jumpSpeed = 15.0f;
    public float maxSpeed = 10.0f;  // 🔹 Set a speed limit
    public float groundCheckDistance = 1.1f; 
    public float moveForce = 2.0f; 

    public FloatingJoystick fjsRight;
    private DashAttack _dash;  // 🔹 Reference to Dash script


    private Rigidbody _rb;
    private bool _isGrounded;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _dash = GetComponent<DashAttack>();

        _rb.freezeRotation = false; 
    }

    void FixedUpdate()
    {
        // float horInput = Input.GetAxisRaw("Horizontal");
        // float vertInput = Input.GetAxisRaw("Vertical");
        if (_dash != null && _dash.IsDashing)
            return; // 🔹 Skip movement logic while dashing
        
        float horInput = fjsRight.Horizontal;
        float vertInput = fjsRight.Vertical;

        if (horInput != 0 || vertInput != 0)
        {
            Vector3 right = target.right;
            Vector3 forward = Vector3.Cross(right, Vector3.up);
            var movement = (right * horInput + forward * vertInput).normalized * moveForce;
            
            _rb.AddForce(movement, ForceMode.Force);
        }

        _isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);

        if (_isGrounded && Input.GetButtonDown("Jump"))
        {
            _rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
        }

        // 🔹 **Limit Maximum Speed**
        if (_rb.linearVelocity.magnitude > maxSpeed)
        {
            _rb.linearVelocity = Vector3.ClampMagnitude(_rb.linearVelocity, maxSpeed);
        }
    }
}