using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    [SerializeField] Transform target;
    public float rotSpeed = 2.5f;
    public float smoothFactor = 0.1f;
    public float followLag = 0.1f; // Delay factor for dynamic follow
    public Vector3 offset = new Vector3(3, 3, -5); // Default offset for dynamic framing

    public FloatingJoystick fjsLeft;  
  
    private float _rotY;  
    private float _rotX;  
    private Vector3 _desiredPosition;  
    private Vector3 _smoothedPosition;  

    void Start()  
    {  
        _rotX = transform.eulerAngles.x;  
        _rotY = transform.eulerAngles.y;  
        _desiredPosition = target.position + offset;  
        _smoothedPosition = _desiredPosition;  
    }  

    void FixedUpdate()  
    {  
        // float mouseX = Input.GetAxisRaw("Mouse X") * rotSpeed;  
        // float mouseY = Input.GetAxisRaw("Mouse Y") * rotSpeed;  
          
        // _rotY += mouseX;  
        // _rotX = Mathf.Clamp(_rotX - mouseY, -50f, 70f); // Prevent extreme angles  
      
        _rotY += fjsLeft.Horizontal;  
        _rotX = Mathf.Clamp(_rotX - fjsLeft.Vertical*(-1f), 0f, 40f); // Prevent extreme angles  

      
        Quaternion rotation = Quaternion.Euler(_rotX, _rotY, 0);  
        _desiredPosition = target.position + rotation * offset; // Dynamic offset shift  

        // Add follow lag to make movement smoother  
        _smoothedPosition = Vector3.Lerp(_smoothedPosition, _desiredPosition, followLag);  
        transform.position = _smoothedPosition;  

        transform.LookAt(target);  
    }

}