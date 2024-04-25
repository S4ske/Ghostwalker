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

    [SerializeField] private bool isChasingEnemy = false;
    [SerializeField] private float chasingDistance = 4f;
    [SerializeField] private float chasingSpeedMult = 2f;

    private NavMeshAgent navMeshAgent;
    private State state;
    private float roamingTime;
    private Vector3 roamPosition;
    private Vector3 startingPosition;

    private State currentState;

    private float roamingSpeed;
    private float chasingSpeed;

    public float health;

    public bool isRangerEnemy = false;
    private float attackingDistance = 1f;
    private float nextAttackTime;
    private float attackRate = 2;
    private event EventHandler OnEnemyAttack;
    public bool IsRunning => navMeshAgent.velocity == Vector3.zero;

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

        roamingSpeed = navMeshAgent.speed;
        chasingSpeed = navMeshAgent.speed * chasingSpeedMult;
    }

    private void Update()
    {
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
                break;
            case State.Chasing:
                ChasingTarget();
                break;
            case State.Attacking:
                break;
            case State.Death:
                break;
            default:
            case State.Idle:
                break;
        }
    }

    private void ChasingTarget()
    {
        if (Player.Instance)
            navMeshAgent.SetDestination(Player.Instance.transform.position);
    }


    private void Roaming()
    {
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