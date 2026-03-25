using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.tvOS;

public class RotationLock : MonoBehaviour
{
    [SerializeField] private Transform attachTo;
    [SerializeField] private float SuspensionStrength;
    private Rigidbody rb;


    // All the Variables i thought i needed
    //[SerializeField] private float slerpSpeed;
    //[SerializeField] private Transform attachFrom;
    //[SerializeField] private float smooth;
    //[SerializeField] private float rotationSpeedChangeMax;
    //[SerializeField] private CarMovement Car;
    //private Vector3 _moveDirection;
    //private int _flips;
    //private Quaternion _rotation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        var delta = attachTo.rotation * Quaternion.Inverse(rb.rotation);
        
        float angle; Vector3 axis;
        delta.ToAngleAxis(out angle, out axis);

        if (float.IsInfinity(axis.x))
        {
            rb.angularVelocity = Vector3.zero;
            return;
        }

        if (angle > 180f)
            angle -= 360f;

        Vector3 angular = (SuspensionStrength * Mathf.Deg2Rad * angle) * axis.normalized;
        
        rb.angularVelocity += angular;
        
        
        // TODO write a damping scriptS
        // TODO add something for rotating to a position based on several forces instead of one point to make the car rotation based on the tires too

S
        ///
        /// I tried to do it myself, i could not do it myself
        /// Thank you internet stranger for showing me how to actually do things
        /// I was very lost but this works almost exactly how i wanted it to
        ///


        //_moveDirection = Vector3.zero;
        //_flips = 0;
        //_moveDirection.x += WhichAngleIsCloser(attachTo.eulerAngles.x, gameObject.transform.eulerAngles.x);
        //_moveDirection.y += WhichAngleIsCloser(attachTo.eulerAngles.y, gameObject.transform.eulerAngles.y);
        //_moveDirection.z += WhichAngleIsCloser(attachTo.eulerAngles.z, gameObject.transform.eulerAngles.z);
        ////if(_flips != 0)
        ////{
        ////    _moveDirection = Vector3.zero;
        ////    _flips = 0;
        ////    Quaternion New = Quaternion.Inverse(attachTo.rotation);
        ////    _moveDirection.x += WhichAngleIsCloser(New.eulerAngles.x, gameObject.transform.eulerAngles.x);
        ////    _moveDirection.y += WhichAngleIsCloser(New.eulerAngles.y, gameObject.transform.eulerAngles.y);
        ////    _moveDirection.z += WhichAngleIsCloser(New.eulerAngles.z, gameObject.transform.eulerAngles.z);
        ////}


        //if (logging) Debug.Log(_moveDirection);
        //if (_moveDirection.magnitude < rotationSpeedChangeMax)
        //{
        //    if (_flips == 0)
        //    {
        //        rb.angularVelocity += new Vector3(xSuspensionStrength * Mathf.Pow(_moveDirection.x / smooth, 3),
        //            ySuspensionStrength * Mathf.Pow(_moveDirection.y / smooth, 3),
        //            zSuspensionStrength * Mathf.Pow(_moveDirection.z / smooth, 3));
        //    }
        //    else
        //    {
        //        rb.angularVelocity += new Vector3(-xSuspensionStrength * Mathf.Pow(_moveDirection.x / smooth, 3),
        //            ySuspensionStrength * Mathf.Pow(_moveDirection.y / smooth, 3),
        //            zSuspensionStrength * Mathf.Pow(_moveDirection.z / smooth, 3));
        //    }
        //    //if (logging)Debug.Log(rb.angularVelocity);
        //}
        //else { rb.rotation = Quaternion.Slerp(rb.rotation, attachTo.rotation, 1f); if (logging) Debug.Log("slerping");}
    }
    //private float WhichAngleIsCloser(float angleTo, float angleFrom)
    //{

    //    //return angleTo - angleFrom;
    //    float angleToHigh = angleTo + 360;
    //    float angleToLow = angleTo - 360;
    //    if (angleFrom - angleTo < -180)
    //    {
    //        while ((angleToLow - angleFrom) < -80) {angleFrom -= 90; _flips++; }
    //        while ((angleToLow - angleFrom) > 80) {angleFrom += 90; _flips++; }
    //        return angleToLow - angleFrom;

    //    }

    //    if (angleFrom - angleTo > 180)
    //    {
    //        while ((angleToHigh - angleFrom) < -80) {angleFrom -= 90; _flips++; }
    //        while ((angleToHigh - angleFrom) > 80) {angleFrom += 90; _flips++; }
    //        return angleToHigh - angleFrom;
            
    //    }
    //    else
    //    {
    //        while ((angleTo - angleFrom) < -80) {angleFrom -= 90; _flips++; }
    //        while ((angleTo - angleFrom) > 80) {angleFrom += 90; _flips++; }
    //        return (angleTo - angleFrom);
    //    }

    //}


    //private Vector3 WhereShouldIGo(Quaternion To, Quaternion From)
    //{
       
    //}
}
