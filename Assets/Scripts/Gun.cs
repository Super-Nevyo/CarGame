using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    [SerializeField] public Transform Target;
    [SerializeField] public CarMovement Car;
    [SerializeField] public float ShootSpeed;
    [SerializeField] public float ShootSpeedChange;


    public void OnEnable()
    {
        Car.ShootEvent += Shoot;
    }
    public void OnDisable()
    {
        Car.ShootEvent -= Shoot;
    }


    public void FixedUpdate()
    {
        RotateTo();
    }

    public virtual void Shoot()
    {

    }
    public virtual void RotateTo()
    {
        
    }
}
