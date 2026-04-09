using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    // this script exists to build new guns off of
    [SerializeField] public Transform Target;
    [SerializeField] public CarMovement Car;
    [SerializeField] public float ShootSpeed;
    [SerializeField] public float ShootSpeedChange;
    // starts listening to the car because all guns should shoot
    public void OnEnable()
    {
        Car.ShootEvent += Shoot;
    }
    public void OnDisable()
    {
        Car.ShootEvent -= Shoot;
    }
    // the RotateTo script needs to be run
    public void FixedUpdate()
    {
        RotateTo();
    }
    // all guns need to shoot but how they do that is up to them
    public abstract void Shoot();
    
    // guns should have the option to rotate
    public abstract void RotateTo();
}
