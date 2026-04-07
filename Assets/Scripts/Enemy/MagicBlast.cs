using UnityEngine;

public class MagicBlast : Bomb, IBombable
{
    [SerializeField] private float fuseTime;
    private Suspension _suspension;
    public Transform Summoner;
    
    void Awake()
    {
        _suspension = GetComponent<Suspension>();
    }
    public void OnBombed()
    {
        _suspension.SetTarget(Summoner);
        BlowUpAfter(fuseTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            BlowUpAfter(1);
        }
    }
}
