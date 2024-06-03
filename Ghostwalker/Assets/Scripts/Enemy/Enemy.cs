using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private State startingState;
    [SerializeField] public bool doChasingEnemy;
    [SerializeField] private bool doAttackingEnemy;
    public GameObject[] teammates;

    [SerializeField] private float chasingSpeed;
    [SerializeField] public Transform PlayerPosition;

    private NavMeshAgent navMeshAgent;
    private State state;
    private Vector3 startingPosition;

    private State currentState;


    public float health;
    private float startHealht;
    private CapsuleCollider2D _capsuleCollider2D;
    private BoxCollider2D _boxCollider2D;

    
    public bool isRangerEnemy;
    [SerializeField] private float attackingDistance = 5f;
    private float nextAttackTime;
    private float attackRate = 2;
    private event EventHandler onEnemyAttack;
    public GameObject potion;
    public bool IsRunning => !navMeshAgent.velocity.Equals(Vector2.zero);
    public bool Attack => isRangerEnemy || attackingDistance 
        >= Vector3.Distance(transform.position, PlayerPosition.position);
    public bool Death => health <= 0;
    public bool TakeHit = false;
    
    private EnemyVisual enemyVisual;
    private bool firstDie = true;

    private EnemySword enemySword;
    

    private enum State
    {
        Idle,
        Chasing,
        Attacking,
        Death
    }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        currentState = startingState;
        enemySword = GetComponent<EnemySword>();
        startHealht = health;

        _boxCollider2D = GetComponent<BoxCollider2D>();
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        StateHandler();
    }


    private void StateHandler()
    {
        if (health <= 0)
        {
            currentState = State.Idle;
            _boxCollider2D.enabled = false;
            _capsuleCollider2D.enabled = false;
            if (firstDie)
            {
                var random = new System.Random();
                if (random.Next(3) == 1)
                    Instantiate(potion, transform.position, Quaternion.identity);
                firstDie = false;
            }
        }
        switch (currentState)
        {
            case State.Chasing:
                ChasingTarget();
                CheckCurrentState();
                break;
            case State.Attacking:
                AttackingTarget();
                CheckCurrentState();
                break;
            case State.Death:
                CheckCurrentState();
                break;
            default:
            case State.Idle:
                CheckCurrentState();
                break;
        }
    }

    private void CheckCurrentState()
    {
        if (health <= 0)
        {
            currentState = State.Death;
            return;
        }
        
        var distanceToPlayer = Vector3.Distance(transform.position, PlayerPosition.position);

        if (distanceToPlayer <= attackingDistance + 4)
        {
            doChasingEnemy = true;
            foreach (var mate in teammates)
                mate.GetComponent<Enemy>().doChasingEnemy = true;
        }
        
        var newState = State.Idle;

        if (doChasingEnemy)
            newState = State.Chasing;
        
        if (doAttackingEnemy)
            newState = State.Attacking;

        if (isRangerEnemy)
            newState = State.Attacking;
        else
        {
            if (distanceToPlayer <= attackingDistance)
                newState = State.Attacking;
        }

        currentState = newState;
    }

    private void AttackingTarget()
    {
        navMeshAgent.ResetPath();
        if (!isRangerEnemy)
        {
            if (!(Time.time > nextAttackTime)) return;
            nextAttackTime = Time.time + attackRate;
            onEnemyAttack?.Invoke(this, EventArgs.Empty);
            
        }
        else
            onEnemyAttack?.Invoke(this, EventArgs.Empty);
    }

    private void ChasingTarget()
    {
        navMeshAgent.ResetPath();
        navMeshAgent.speed = chasingSpeed;
        ChangFacingDirection(transform.position, PlayerPosition.position);
        navMeshAgent.SetDestination(PlayerPosition.position);
    }

    private void ChangFacingDirection(Vector3 sourcePosition, Vector3 targetPosition)
    {
        transform.rotation = sourcePosition.x > targetPosition.x 
            ? Quaternion.Euler(0, -180, 0)
            : Quaternion.Euler(0, 0, 0);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (!doChasingEnemy)
        {
            doChasingEnemy = true;
            foreach (var mate in teammates)
                mate.GetComponent<Enemy>().doChasingEnemy = true;
        }
    }
}