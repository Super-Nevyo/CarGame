using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Rat : MonoBehaviour, IBombable
{

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float sightDistance;
    [SerializeField] private float sightAngle;
    [SerializeField] public Transform MaxSpawnAreaNode;
    [SerializeField] public Transform MinSpawnAreaNode;
    [SerializeField] private float waitTime;
    [SerializeField] private AudioClip noticeSound;
    [SerializeField] private float bombTime;

    private Vector3 currentTarget;

    private Bomb _bomb;

    private EnemyState _currentState;
    private bool _waiting = false;

    private void Awake()
    {
        // take a note of the bomb attached to the rat
        _bomb = GetComponentInChildren<Bomb>();
        _currentState = EnemyState.IDLE;
        
    }
    void FixedUpdate()
    {
        
        if (_currentState == EnemyState.IDLE)
        {
            if (!_waiting) StartCoroutine(WaitAndGo(waitTime));
            // stop all corutines is run to stop the WaitAndGo from triggering if the player runs away
            if (IsTargetSpotable(playerTransform)) { 
                _currentState = EnemyState.CHASE; 
                AudioSource.PlayClipAtPoint(noticeSound, transform.position); 
                StopAllCoroutines(); 
                _bomb.BlowUpAfter(bombTime);  }
        }
        if(_currentState == EnemyState.WANDER)
        {
            if(agent.remainingDistance <= 0.2) _currentState = EnemyState.IDLE;
            if (IsTargetSpotable(playerTransform)) { 
                _currentState = EnemyState.CHASE; 
                AudioSource.PlayClipAtPoint(noticeSound, transform.position); 
                _bomb.BlowUpAfter(bombTime); }
        }
        if(_currentState == EnemyState.CHASE)
        {
            // setting destination every fixed update is bad but it should only be ran every now and then
            agent.SetDestination(playerTransform.position);
            // the rat lights its bombs fuse, this is why there is no return to wandering or idle
        }
    }

    public void OnBombed()
    {
        // if the bomb on the rat isnt blowing up already, it should start
        _bomb.BlowUpAfter(2);
        // the bomb gets let go of by the rat and is given a Rigidbody so it does normal bomb behaviour
        _bomb.AddComponent<Rigidbody>();
        _bomb.transform.parent = null;
        // kill the rat
        Destroy(gameObject);
    }
    // this is almost the same as we made in class, instead of a number of points the enemy might walk to, the rat will go to a random place within a box
    private void ChooseAPointInBoundsAndMove()
    {
        currentTarget = new Vector3(Random.Range(MaxSpawnAreaNode.position.x, MinSpawnAreaNode.position.x), Random.Range(MaxSpawnAreaNode.position.y, MinSpawnAreaNode.position.y), Random.Range(MaxSpawnAreaNode.position.z, MinSpawnAreaNode.position.z));
        agent.SetDestination(currentTarget);
    }
    // pretty much just what we learned in class
    private IEnumerator WaitAndGo(float waitTime)
    {
        _waiting = true;
        yield return new WaitForSeconds(waitTime);
        _currentState = EnemyState.WANDER;
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
