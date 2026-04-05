using UnityEngine;

public class RotationLock : MonoBehaviour
{
    [SerializeField] private Transform attachTo;
    [SerializeField] private float SuspensionStrength;
    private Rigidbody rb;
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


        ///
        /// I tried to do it myself, i could not do it myself
        /// Thank you internet stranger for showing me how to actually do things
        /// I was very lost but this works almost exactly how i wanted it to
        ///


    }
    
}
