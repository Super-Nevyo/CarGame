using UnityEngine;

public class Cannon : Gun
{

    [SerializeField] private GameObject projectile;
    private GameObject _bomb;

    // this is pretty much the same as what we learned in class with the arrow but with a different projectile
    protected override void Shoot()
    {
        _bomb = Instantiate(projectile, transform.position, transform.rotation);
        // shootspeed becomes stronger the more the gun is pointed up because it makes for more interesing trickshots
        _bomb.GetComponent<Rigidbody>().AddForce((ShootSpeed + Target.position.y * ShootSpeedChange) * (Target.position - transform.position).normalized);
        _bomb.GetComponent<Bomb>().BlowUpAfter(2f);
    }
    // we learned how to do this in class, it is a pretty simple rotate script and functions well
    protected override void RotateTo()
    {
        transform.rotation = Quaternion.LookRotation(Target.position - transform.position);
    }
}
