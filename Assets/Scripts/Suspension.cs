using UnityEngine;

public class Suspension : MonoBehaviour
{
    [SerializeField] private Transform[] attachTo;
    [SerializeField] private Transform[] attachFrom;
    [SerializeField] private float xSuspensionStrength;
    [SerializeField] private float ySuspensionStrength;
    [SerializeField] private float zSuspensionStrength;
    [SerializeField] private float dampingStrength;

    private Rigidbody rb;
    private Vector3 _moveDirection;
    private bool _yesDamping = true;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (attachTo[0].GetComponentInParent<Rigidbody>() == null) _yesDamping = false;
    }

    void FixedUpdate()
    {
        // this is not normalized because i want the strength to get stronger the farther the objects are
        // reset the _move direction so the += sums the new values instead of the old ones
        _moveDirection = Vector3.zero;
        // in this case the attach from point is the center of the object but you can attach it to as many points as you like
        if (attachFrom.Length == 0)
        {
            foreach (Transform attach in attachTo)
            {
                _moveDirection += attach.position - gameObject.transform.position;
            }
        }
        // in  this case there are several attach from points for situations like the tires of a car where the body needs to connect from the wheel hub to the tire
        // you can attach several attachTo points to a single attach from point 
        else if (attachFrom.Length == attachTo.Length)
        {
            for (int a = 0; a < attachFrom.Length; a++)
            {
                _moveDirection += attachTo[a].position - attachFrom[a].position;
            }
        }
        else {Debug.LogError("Suspension only supports attaching together");
        }
        // if the distance is too big it is clamped to not have too aggressive bouncing
        _moveDirection = Vector3.ClampMagnitude(_moveDirection, 0.5f);
        rb.linearVelocity += new Vector3(Mathf.Pow(_moveDirection.x,3) * xSuspensionStrength, Mathf.Pow(_moveDirection.y,3) * ySuspensionStrength,
            Mathf.Pow(_moveDirection.z,3) * zSuspensionStrength);
        // reduces linear velocity by the amount damped

        if(_yesDamping)
        foreach(Transform attach in attachTo)
        {
                rb.linearVelocity -= CalculateDamping(attach.GetComponentInParent<Rigidbody>().linearVelocity, rb.linearVelocity);
        }
    }
    // Calculates the linear damping based on the linear velocity of two Tigidbodys
    private Vector3 CalculateDamping(Vector3 LVFrom, Vector3 LVTo)
    {
        return (-LVFrom + LVTo) / dampingStrength;
    }
    // this exists to make new objects attach to a thing
    public void SetTarget(Transform target)
    {
        attachTo[0] = target;
        // need to make sure the new attachment point has a Rigidbody because otherwise there will be a bunch of errors when the Magic Blast swiches target
        _yesDamping = attachTo[0].GetComponentInParent<Rigidbody>() != null;
    }
}
