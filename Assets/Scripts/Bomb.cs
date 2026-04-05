using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private ParticleSystem playExplosion;
    [SerializeField] private ParticleSystem playFuseFlame;
    [SerializeField] float circleCastSize = 2;
    [SerializeField] float bombStrength = 200;
    [SerializeField] AudioClip explosionSound;

    private RaycastHit[] _raycastHits;

    private IBombable bombed;
    private bool _fuseLit = false;

    // does a sphere cast, triggers on bombed of i bombables and sends all rigid bodys away
    void BlowUp()
    {
        playExplosion.Play();
        AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        _raycastHits = Physics.SphereCastAll(transform.position,
            circleCastSize,
            Vector3.up,
            0,
            ~0);
        foreach(RaycastHit hit in _raycastHits)
        {
            Debug.Log(hit.collider.name);
            bombed = hit.collider.GetComponent<IBombable>();
            bombed?.OnBombed();
            if (hit.rigidbody != null) 
            {
                hit.rigidbody.AddForce(bombStrength * (hit.transform.position - transform.position).normalized);
                
            }
            
        }
        Destroy(gameObject, 0.2f);
    }
    // checks if bomb is already going off, if it isnt: turn on the particles for the lit fuse and bluws up the bomb some time after
    public void BlowUpAfter(float time)
    {
        if (_fuseLit) return;
        _fuseLit = true;
        playFuseFlame.Play();
        Invoke(nameof(BlowUp), time);
    }
}
