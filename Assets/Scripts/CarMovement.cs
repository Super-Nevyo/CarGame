using UnityEngine;
using UnityEngine.InputSystem;

public class CarMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody poweredTire1;
    [SerializeField] private Rigidbody poweredTire2;
    [SerializeField] private Rigidbody turningTire1;
    [SerializeField] private Rigidbody turningTire2;
    [SerializeField] private float speed;
    private Rigidbody rb;

    private float _acceleration;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Time.timeScale = 0.5f;
    }

    void OnMove(InputValue value)
    {
        _acceleration = value.Get<Vector2>().y;
    }

    // Update is called once per frame
    void Update()
    { 
        poweredTire1.angularVelocity += (speed * _acceleration) * gameObject.transform.right;
        poweredTire2.angularVelocity += (speed * _acceleration) * gameObject.transform.right;
        //poweredTire1.rotation = Quaternion.Euler(poweredTire1.rotation.x, transform.rotation.y, transform.rotation.z + 90);
        //poweredTire2.rotation = Quaternion.Euler(poweredTire2.rotation.x, transform.rotation.y, transform.rotation.z + 90);
    }
}
