using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    // this script exists to build new guns off of
    [SerializeField] protected Transform Target;
    [SerializeField] protected CarMovement Car;
    [SerializeField] protected float ShootSpeed;
    [SerializeField] protected float ShootSpeedChange;
    // starts listening to the car because all guns should shoot
    protected void OnEnable()
    {
        Car.ShootEvent += Shoot;
    }
    protected void OnDisable()
    {
        Car.ShootEvent -= Shoot;
    }
    // the RotateTo script needs to be run
    protected void FixedUpdate()
    {
        RotateTo();
    }
    // all guns need to shoot but how they do that is up to them
    protected abstract void Shoot();

    // guns should have the option to rotate
    protected abstract void RotateTo();
}
