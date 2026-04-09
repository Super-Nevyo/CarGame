using UnityEngine;
using UnityEngine.AI;

public abstract class BaseEnemy : MonoBehaviour, IBombable
{
    protected NavMeshAgent agent;
    protected Transform playerTransform;
    [SerializeField] protected float sightDistance;
    [SerializeField] protected float sightAngle;
    [SerializeField] protected Transform maxMoveAreaNode;
    [SerializeField] protected Transform minMoveAreaNode;
    [SerializeField] protected float WaitTime;


    private EnemyState _currentState;
    private Vector3 currentTarget;

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        _currentState = EnemyState.IDLE;
    }

    private void FixedUpdate()
    {
        if (_currentState == EnemyState.IDLE)
        {
            IdleAction();
        }
        if (_currentState == EnemyState.WANDER)
        {
            WanderAction();
        }
        if (_currentState == EnemyState.CHASE)
        {
            ChaseAction();
        }
        if(_currentState == EnemyState.SHOOT)
        {
            ShootAction();
        }
        if(_currentState == EnemyState.DYING)
        {
            DieAction();
        }
    }
    // functions to be played in the fixed update to do actions while in different states
    protected virtual void IdleAction()
    {

    }
    protected virtual void WanderAction()
    {

    }
    protected virtual void ChaseAction()
    {

    }
    protected virtual void ShootAction()
    {

    }
    protected virtual void DieAction()
    {

    }
    public virtual void OnBombed()
    {

    }
    protected void ChangeState(EnemyState NewState)
    {
        _currentState = NewState;
        // this would be where i would change animation state when i add animations
        if(_currentState == EnemyState.DYING)
        {
            StopAllCoroutines();
            agent.SetDestination(transform.position);
        }
    }
    // this is almost the same as we made in class, instead of a number of points the enemy might walk to, the enemy will go to a random place within a box
    protected void ChooseAPointInBoundsAndMove()
    {
        currentTarget = new Vector3(Random.Range(maxMoveAreaNode.position.x, minMoveAreaNode.position.x), Random.Range(maxMoveAreaNode.position.y, minMoveAreaNode.position.y), Random.Range(maxMoveAreaNode.position.z, minMoveAreaNode.position.z));
        agent.SetDestination(currentTarget);
    }
    //this would be helpful for a ranged enemy to get close but not too close and it is used by the rat wizard to be annoying but sometimes hitable
    protected void ApprochToDistance(float Distance)
    {
        currentTarget = Distance * (-playerTransform.position + transform.position).normalized + playerTransform.position;
        agent.SetDestination(currentTarget);
    }
    // this is pretty much what we learned in class but in 1 method instead of 2 and slightly different formatting
    protected bool IsTargetSpotable(Transform target)
    {
        if (Vector3.Magnitude(target.position - transform.position) > sightDistance) return false;
        if (Mathf.Abs(Vector3.Angle(transform.forward, target.position - transform.position)) > sightAngle) { return false; }
        return true;
    }
    public void SetMoveBounds(Transform max,  Transform min)
    {
        maxMoveAreaNode = max;
        minMoveAreaNode = min;
    }
}
