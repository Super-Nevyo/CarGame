using UnityEngine;

public class Cannon : Gun
{

    [SerializeField] private GameObject projectile;
    private GameObject _bomb;

    // this is pretty much the same as what we learned in class with the arrow but with a different projectile
    public override void Shoot()
    {
        _bomb = Instantiate(projectile, transform.position, transform.rotation);
        _bomb.GetComponent<Rigidbody>().AddForce((ShootSpeed + Target.position.y * ShootSpeedChange) * (Target.position - transform.position).normalized);
        _bomb.GetComponent<Bomb>().BlowUpAfter(2f);
    }
    public override void RotateTo()
    {
        transform.rotation = Quaternion.LookRotation(Target.position - transform.position);
    }
}
