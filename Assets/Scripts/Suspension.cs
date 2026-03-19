using UnityEngine;

public class Suspension : MonoBehaviour
{
    [SerializeField] private Transform[] attachTo;
    [SerializeField] private Transform[] attachFrom;
    [SerializeField] private float xSuspensionStrength;
    [SerializeField] private float ySuspensionStrength;
    [SerializeField] private float zSuspensionStrength;

    private Rigidbody rb;
    private Vector3 _moveDirection;
    private Quaternion _rotation;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // this is not normalized because i want the strength to get stronger the farther the objects are
        _moveDirection = Vector3.zero;
        if (attachFrom.Length == 0)
        {
            foreach (Transform attach in attachTo)
            {
                _moveDirection += attach.position - gameObject.transform.position;
            }
        }
        else if (attachFrom.Length == attachTo.Length)
        {
            for (int a = 0; a < attachFrom.Length; a++)
            {
                _moveDirection += attachTo[a].position - attachFrom[a].position;
            }
        }
        else Debug.LogError("Suspension only supports attaching together");
        _moveDirection = Vector3.ClampMagnitude(_moveDirection, 1f);
        rb.linearVelocity += new Vector3(Mathf.Atan(_moveDirection.x) * xSuspensionStrength, Mathf.Atan(_moveDirection.y) * ySuspensionStrength,
            Mathf.Atan(_moveDirection.z) * zSuspensionStrength);
        //_rotation = gameObject.transform.rotation - attachTo.rotation;
    }
}
