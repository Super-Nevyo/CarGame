using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private ParticleSystem playExplosion;
    [SerializeField] float circleCastSize = 2;
    [SerializeField] float bombStrength = 200;
    private RaycastHit[] _raycastHits;

    private Rigidbody rb;
    private IBombable bombed;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Invoke(nameof(BlowUp), 2);
    }

    void Update()
    {
        
    }

    void BlowUp()
    {
        playExplosion.Play();
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
}
