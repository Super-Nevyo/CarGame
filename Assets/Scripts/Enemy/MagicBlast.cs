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
    // when you hit a magic blast, it stops it from targeting the player and starts targeting the caster
    public void OnBombed()
    {
        _suspension.SetTarget(Summoner);
        BlowUpAfter(fuseTime);
    }
    // if it touches the player it blows up with just enough time to see it is there
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            BlowUpAfter(1);
        }
    }
}
