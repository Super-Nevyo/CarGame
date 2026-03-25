using UnityEngine;

public class Cannon : MonoBehaviour
{

    [SerializeField] Transform Target;
    [SerializeField] private CarMovement car;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float shootSpeed;

    private GameObject _bomb;

    void OnEnable()
    {
        car.ShootEvent += Shoot;
    }
    void OnDisable()
    {
        car.ShootEvent -= Shoot;
    }


    void FixedUpdate()
    {
        transform.rotation = Quaternion.LookRotation(Target.position - transform.position);
    }
    // this is pretty much the same as what we learned in class with the arrow but with a different projectile
    private void Shoot()
    {
        _bomb = Instantiate(projectile, transform.position, transform.rotation);
        _bomb.GetComponent<Rigidbody>().AddForce(shootSpeed * (Target.position - transform.position).normalized);
    }

}
