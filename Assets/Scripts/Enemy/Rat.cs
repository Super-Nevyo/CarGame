using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Rat : BaseEnemy
{
    [SerializeField] private AudioClip noticeSound;
    [SerializeField] private float bombTime;
    private Bomb _bomb;
    private bool _waiting = false;

    public override void Awake()
    {
        base.Awake();
        // take a note of the bomb attached to the rat
        _bomb = GetComponentInChildren<Bomb>();
        
    }
    /// <summary>
    /// these are called in the base enemy fixed update to be run when in different states
    /// </summary>
    public override void IdleAction()
    {
        if (!_waiting) StartCoroutine(WaitAndGo(WaitTime));
        // stop all corutines is run to stop the WaitAndGo from triggering if the player runs away
        if (IsTargetSpotable(playerTransform))
        {
            ChangeState(EnemyState.CHASE);
            AudioSource.PlayClipAtPoint(noticeSound, transform.position);
            StopAllCoroutines();
            _bomb.BlowUpAfter(bombTime);
        }
    }
    public override void WanderAction()
    {
        if (agent.remainingDistance <= 0.2) ChangeState(EnemyState.IDLE);
        if (IsTargetSpotable(playerTransform))
        {
            ChangeState(EnemyState.CHASE);
            AudioSource.PlayClipAtPoint(noticeSound, transform.position);
            _bomb.BlowUpAfter(bombTime);
        }
    }
    public override void ChaseAction()
    {
        // setting destination every fixed update is bad but it should only be ran every now and then
        agent.SetDestination(playerTransform.position);
        // the rat lights its bombs fuse, this is why there is no return to wandering or idle
    }

    public override void OnBombed()
    {
        // if the bomb on the rat isnt blowing up already, it should start
        _bomb.BlowUpAfter(2);
        // the bomb gets let go of by the rat and is given a Rigidbody so it does normal bomb behaviour
        _bomb.AddComponent<Rigidbody>();
        _bomb.transform.parent = null;
        // kill the rat
        Destroy(gameObject);
    }
    
    // pretty much just what we learned in class
    private IEnumerator WaitAndGo(float waitTime)
    {
        _waiting = true;
        yield return new WaitForSeconds(waitTime);
        ChangeState(EnemyState.WANDER);
        ChooseAPointInBoundsAndMove();
        _waiting = false;
    }
    // this is pretty much what we learned in class but in 1 method instead of 2 and slightly different formatting
    private bool IsTargetSpotable(Transform target)
    {//Debug.Log(Vector3.Magnitude(target.position - transform.position));
        if (Vector3.Magnitude(target.position - transform.position) > sightDistance) return false;
        if (Mathf.Abs(Vector3.Angle(transform.forward, target.position - transform.position)) > sightAngle) { Debug.Log(Vector3.Angle(transform.forward, target.position - transform.position)); return false; }
        return true;
    }
}
