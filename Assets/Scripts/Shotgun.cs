using UnityEngine;

public class Shotgun : Gun
{

    [SerializeField] private ParticleSystem playShotgunBlast;
    [SerializeField] private Vector3 boxCastHalf;
    [SerializeField] private float shotStrength;
    [SerializeField] private float shotStartDistance;
    private RaycastHit[] _raycastHits;
    private IBombable bombed;
    
    public override void Shoot()
    {
        playShotgunBlast.Play();
        _raycastHits = Physics.BoxCastAll(transform.position + shotStartDistance * transform.forward, boxCastHalf, transform.forward, transform.rotation, 0, ~0);
        foreach (RaycastHit hit in _raycastHits)
        {
            bombed = hit.collider.GetComponent<IBombable>();
            bombed?.OnBombed();
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(shotStrength * (hit.transform.position - transform.position).normalized);

            }

        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + shotStartDistance * transform.forward, 2 * boxCastHalf);
    }
}
