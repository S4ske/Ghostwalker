using System;
using UnityEngine;
using UnityEngine.AI;
using GhostWalker.RandomDirecton;

public class Enemy : MonoBehaviour
{
    [SerializeField] private State startingState;
    [SerializeField] private float roamingTimerMax = 4f;

    [SerializeField] private bool doChasingEnemy;
    [SerializeField] private bool doAttackingEnemy;

    [SerializeField] private float chasingSpeed;
    [SerializeField] private Transform PlayerPosition;

    private NavMeshAgent navMeshAgent;
    private State state;
    private Vector3 startingPosition;

    private State currentState;


    public float health;

    
    public bool isRangerEnemy;
    private float attackingDistance = 2f;
    private float nextAttackTime;
    private float attackRate = 2;
    private event EventHandler onEnemyAttack;

    public bool IsRunning => !navMeshAgent.velocity.Equals(Vector2.zero);
    public bool Attack => isRangerEnemy || attackingDistance >= Vector3.Distance(transform.position, PlayerPosition.position);
    public bool Death => health <= 0;
    public bool TakeHit => health <= 0;
    

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
    }

    private void Update()
    {
        Debug.Log(navMeshAgent.velocity);
        StateHandler();
        // if (health <= 0)
        //     Destroy(gameObject);
    }


    private void StateHandler()
    {
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
        var distanceToPlayer = Vector3.Distance(transform.position, PlayerPosition.position);
        
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
        navMeshAgent.SetDestination(PlayerPosition.position);
    }

    public void TakeDamage(float damage) => health -= damage;
}