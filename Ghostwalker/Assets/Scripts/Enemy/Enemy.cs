using System;
using UnityEngine;
using UnityEngine.AI;
using GhostWalker.RandomDirecton;

public class Enemy : MonoBehaviour
{
    [SerializeField] private State startingState;
    [SerializeField] private float roamingDistanceMax = 7f;
    [SerializeField] private float roamingDistanceMin = 1f;
    [SerializeField] private float roamingTimerMax = 4f;

    [SerializeField] private bool doChasingEnemy;
    [SerializeField] private bool doRoamingEnemy;
    [SerializeField] private bool doAttackingEnemy;

    [SerializeField] private float chasingSpeed;
    [SerializeField] private Transform PlayerPosition;

    private NavMeshAgent navMeshAgent;
    private State state;
    private float roamingTime;
    private Vector3 roamPosition;
    private Vector3 startingPosition;

    private State currentState;


    public float health;

    
    public bool isRangerEnemy;
    private float attackingDistance = 1f;
    private float nextAttackTime;
    private float attackRate = 2;
    private event EventHandler onEnemyAttack;

    public bool IsRunning => !navMeshAgent.velocity.Equals(Vector2.zero);
    

    private enum State
    {
        Idle,
        Roaming,
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
        if (health <= 0)
            Destroy(gameObject);
    }


    private void StateHandler()
    {
        switch (currentState)
        {
            case State.Roaming:
                roamingTime -= Time.deltaTime;
                if (roamingTime < 0)
                {
                    Roaming();
                    roamingTime = roamingTimerMax;
                }

                CheckCurrentState();
                break;
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
        if (doRoamingEnemy)
            newState = State.Roaming;
        if (doAttackingEnemy)
            newState = State.Attacking;
        
        // if (!isRangerEnemy)
        // {
        //     if (distanceToPlayer <= attackingDistance)
        //         newState = State.Attacking;
        // }
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


    private void Roaming()
    {
        roamingTime = 0f;
        navMeshAgent.speed = chasingSpeed - 2;
        startingPosition = transform.position;
        roamPosition = GetRoamingPosition();
        navMeshAgent.SetDestination(roamPosition);
    }


    private Vector3 GetRoamingPosition()
    {
        return startingPosition + RandomDirection.GetRandomDirection() *
            UnityEngine.Random.Range(roamingDistanceMin, roamingDistanceMax);
    }

    public void TakeDamage(float damage) => health -= damage;
}