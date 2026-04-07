using UnityEngine;
using UnityEngine.AI;

public abstract class BaseEnemy : MonoBehaviour, IBombable
{
    protected NavMeshAgent agent;
    protected Transform playerTransform;
    [SerializeField] public float sightDistance;
    [SerializeField] public float sightAngle;
    [SerializeField] public Transform MaxMoveAreaNode;
    [SerializeField] public Transform MinMoveAreaNode;
    [SerializeField] public float WaitTime;


    private EnemyState _currentState;
    private Vector3 currentTarget;

    public virtual void Awake()
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
    public virtual void IdleAction()
    {

    }
    public virtual void WanderAction()
    {

    }
    public virtual void ChaseAction()
    {

    }
    public virtual void ShootAction()
    {

    }
    public virtual void DieAction()
    {

    }
    public virtual void OnBombed()
    {

    }
    public void ChangeState(EnemyState NewState)
    {
        _currentState = NewState;
        if(_currentState == EnemyState.DYING)
        {
            StopAllCoroutines();
            agent.SetDestination(transform.position);
        }
    }
    // this is almost the same as we made in class, instead of a number of points the enemy might walk to, the enemy will go to a random place within a box
    public void ChooseAPointInBoundsAndMove()
    {
        currentTarget = new Vector3(Random.Range(MaxMoveAreaNode.position.x, MinMoveAreaNode.position.x), Random.Range(MaxMoveAreaNode.position.y, MinMoveAreaNode.position.y), Random.Range(MaxMoveAreaNode.position.z, MinMoveAreaNode.position.z));
        agent.SetDestination(currentTarget);
    }
    public void ApprochToDistance(float Distance)
    {
        currentTarget = Distance * (-playerTransform.position + transform.position).normalized + playerTransform.position;
        agent.SetDestination(currentTarget);
    }
}
