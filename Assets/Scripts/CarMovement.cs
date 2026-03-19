using UnityEngine;
using UnityEngine.InputSystem;

public class CarMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody poweredTire1;
    [SerializeField] private Rigidbody poweredTire2;
    [SerializeField] private Rigidbody turningTire1;
    [SerializeField] private Rigidbody turningTire2;
    private Rigidbody rb;

    private float _acceleration;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnMove(InputValue value)
    {
        _acceleration = value.Get<Vector2>().y;
    }

    // Update is called once per frame
    void Update()
    { 
        poweredTire1.angularVelocity += new Vector3( _acceleration, 0f, 0f);
        poweredTire2.angularVelocity += new Vector3(_acceleration, 0f, 0f);
        poweredTire1.rotation = Quaternion.Euler(poweredTire1.rotation.x, rb.rotation.y, rb.rotation.z + 90).normalized;
        poweredTire2.rotation = Quaternion.Euler(poweredTire2.rotation.x, rb.rotation.y, rb.rotation.z + 90).normalized;
    }
}
