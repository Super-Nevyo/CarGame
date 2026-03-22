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
    private void Shoot()
    {
        _bomb = Instantiate(projectile, transform.position, transform.rotation);
        _bomb.GetComponent<Rigidbody>().AddForce(shootSpeed * (Target.position - transform.position).normalized);
    }

}
