using System.Collections;
using UnityEngine;

public class RatWizard : BaseEnemy
{
    [SerializeField] private int health = 3;
    [SerializeField] private int value = 33;

    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip deathSound;
    
    [SerializeField] private BaseEnemy minionToBeSpawned;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float invincibilityTime;
    [SerializeField] private int spawnedPerCycle;
    [SerializeField] private float approchTo;
    [SerializeField] private ParticleSystem magicOrbSpawnParticles;
    [SerializeField] private float tauntDuration;
    [SerializeField] private GameObject forceField;
    [SerializeField] private ParticleSystem deathParticles;

    private bool _waiting = false;
    private int _choice;
    private int _spawnedNum;
    private BaseEnemy _spawnedMinionTemp;
    private GameObject _spawnedBlastTemp;
    private bool _isInvincible = false;
    private AudioSource _audioSource;
    private int _numSpawnBlasts;

    public override void Awake()
    {
        base.Awake();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = hitSound;
        forceField.SetActive(false);
    }
    // wait and then swap to a different action
    public override void IdleAction()
    {
        if(!_waiting)
        StartCoroutine(WaitAndChoose(WaitTime));
    }
    // wander around the arena and spawn rats
    public override void WanderAction()
    {
        if (_spawnedNum >= spawnedPerCycle) ChangeState(EnemyState.IDLE);
        if (agent.remainingDistance <= 1.2)
        {
            SpawnMinion();
            ChooseAPointInBoundsAndMove();
        }
    }
    public override void ChaseAction()
    {
        Debug.Log("chase");
        StartCoroutine(TauntFor(tauntDuration));
        ApprochToDistance(approchTo);
    }
    public override void ShootAction()
    {
        Debug.Log("shooting");
        StartCoroutine(ShootAfter(2));
        ChangeState(EnemyState.IDLE);
    }
    public override void DieAction()
    {
        // spin while dying
        transform.Rotate(Vector3.up * 3);
    }
    // when hit by a bomb or the like, score goes up, health goes down, checks if they should be dead, starts invicibility timer, doubles movement speed, play rat scream sound
    public override void OnBombed()
    {
        if (!_isInvincible)
        {
            agent.speed += agent.speed;
            GameManager.instance.UpdateScore(value);
            health--;
            if (health <= 0)
            {
                // value = 0 so you cant keep hitting the rat wizard when they are in their dying animation
                value = 0;
                // death means dying state, death sound and death particles
                ChangeState(EnemyState.DYING);
                _audioSource.clip = deathSound;
                deathParticles.Play();
                StartCoroutine(DieAfter(3));
            }
            else
            {
                StartCoroutine(Invincibility(invincibilityTime));
            }
            _audioSource.Play();
        }
    }
    private void SpawnMinion()
    {
        _spawnedMinionTemp = Instantiate(minionToBeSpawned, transform.position, Quaternion.identity);
        _spawnedMinionTemp.MaxMoveAreaNode = MaxMoveAreaNode;
        _spawnedMinionTemp.MinMoveAreaNode = MinMoveAreaNode;
        _spawnedNum++;
    }
    // IEnumerator to wait for some time and then choose an action
    private IEnumerator WaitAndChoose(float waitTime)
    {
        _waiting = true;
        yield return new WaitForSeconds(waitTime + Random.Range(-waitTime/2,waitTime/2));
        // if: 0, keep in idle for longer; 1, wander around spawning rats; 2, orbit the player being annoying; 3 move to a place and shoot at the player
        _choice = Random.Range(0, 4);
        switch (_choice)
        {
            case 0:
                break;
            case 1:
                ChangeState(EnemyState.WANDER);
                _spawnedNum = 0;
                break;
            case 2:
                ChangeState(EnemyState.CHASE);
                break;
            case 3:
                ChangeState(EnemyState.SHOOT);
                break;
        }
        _waiting = false;
    }
    private IEnumerator Invincibility(float ITime)
    {
        _isInvincible = true;
        forceField.SetActive(true);
        yield return new WaitForSeconds(ITime);
        _isInvincible = false;
        forceField.SetActive(false);
    }
    private IEnumerator ShootAfter(float shootTime)
    {
        magicOrbSpawnParticles.Play();
        yield return new WaitForSeconds(shootTime);
        _numSpawnBlasts = Random.Range(2, 5);
        for (int i = 1; i < _numSpawnBlasts; i++)
        {
            _spawnedBlastTemp = Instantiate(projectile, transform.position + 2 * i * Vector3.up, Quaternion.identity);
            _spawnedBlastTemp.GetComponent<Suspension>().SetTarget(playerTransform);
            _spawnedBlastTemp.GetComponent<MagicBlast>().Summoner = transform;
        }
    }
    private IEnumerator TauntFor(float time)
    {
        yield return new WaitForSeconds(time); 
        ChangeState(EnemyState.IDLE);
    }
    private IEnumerator DieAfter(float time)
    {
        yield return new WaitForSeconds(time);
        GameManager.instance.UpdateGameState(GameStates.END);
        Destroy(gameObject);
    }
}
