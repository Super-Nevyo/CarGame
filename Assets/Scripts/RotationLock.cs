using UnityEngine;

public class RotationLock : MonoBehaviour
{
    [SerializeField] private Transform[] attachTo;
    [SerializeField] private Transform[] attachFrom;
    [SerializeField] private float xSuspensionStrength;
    [SerializeField] private float ySuspensionStrength;
    [SerializeField] private float zSuspensionStrength;
    [SerializeField] private float smooth;
    [SerializeField] private float rotationSpeedChangeMax;
    [SerializeField] private bool logging;

    private Rigidbody rb;
    private Vector3 _moveDirection;
    private Quaternion _rotation;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        _moveDirection = Vector3.zero;
        if (attachFrom.Length == 0)
        {
            foreach (Transform attach in attachTo)
            {
                
                //_moveDirection += attach.eulerAngles - gameObject.transform.eulerAngles;
                _moveDirection.x += WhichAngleIsCloser(attach.eulerAngles.x, gameObject.transform.eulerAngles.x);
                _moveDirection.y += WhichAngleIsCloser(attach.eulerAngles.y, gameObject.transform.eulerAngles.y);
                _moveDirection.z += WhichAngleIsCloser(attach.eulerAngles.z, gameObject.transform.eulerAngles.z);
            }
        }
        else if (attachFrom.Length == attachTo.Length)
        {
            for (int a = 0; a < attachFrom.Length; a++)
            {
                _moveDirection += attachTo[a].eulerAngles - attachFrom[a].eulerAngles;
            }
        }
        else Debug.LogError("Suspension only supports attaching together");

        if (-rotationSpeedChangeMax < _moveDirection.magnitude && _moveDirection.magnitude < rotationSpeedChangeMax)
        {
            rb.angularVelocity += new Vector3(xSuspensionStrength * Mathf.Pow(_moveDirection.x / smooth, 3),
                ySuspensionStrength * Mathf.Pow(_moveDirection.y / smooth, 3),
                zSuspensionStrength * Mathf.Pow(_moveDirection.z / smooth, 3));
            if(logging)Debug.Log(rb.angularVelocity);
        }
    }

    private float WhichAngleIsCloser(float angleTo, float angleFrom)
    {
        float angleToHigh =  angleTo + 360;
        float angleToLow = angleTo - 360;
        if (angleFrom - angleTo < -180)
        {
            return angleToLow - angleFrom;
        }

        if (angleFrom - angleTo > 180)
        {
            return angleToHigh - angleFrom;
        }
        else return angleTo - angleFrom;




        //if (Mathf.Abs(-angleFrom + angleTo) > Mathf.Abs(-angleFrom + angleToLow)) return(angleToLow - angleFrom);
        //if (Mathf.Abs(-angleFrom + angleTo) < Mathf.Abs(-angleFrom + angleToHigh)) return(angleTo - angleFrom);
        //return(angleToHigh - angleFrom);
    }
}
