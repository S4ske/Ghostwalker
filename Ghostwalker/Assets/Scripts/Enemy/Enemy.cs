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
    
    public bool IsRunning => navMeshAgent.velocity == Vector3.zero;

    private enum State
    {
        Idle,
        Roaming,
        Chasing,
        Atacking
    }
    

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        state = startingState;
        
        roamingSpeed = navMeshAgent.speed;
        chasingSpeed = navMeshAgent.speed * chasingSpeedMult;
    }

    private void Update()
    {
        StateHandle();
        if (health <= 0)
            Destroy(gameObject);
    }

    private void StateHandle()
    {
        switch (state)
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
            
            case State.Atacking:
                
            default:
            case State.Idle:
                break;
        }
    }

    private void CheckCurrentState()
    {
        var distanceToPlayer = Vector3.Distance(transform.position, Player.Instance.transform.position);
        var newState = State.Roaming;
        
        
        if (isChasingEnemy && distanceToPlayer <= chasingDistance)
                newState = State.Chasing;
        
        if (newState != currentState)
        {
            if (newState == State.Chasing)
            {
                navMeshAgent.ResetPath();
                navMeshAgent.speed = chasingSpeed;
            }
            else if (newState == State.Roaming)
            {
                roamingTime = 1f;
                navMeshAgent.speed = roamingSpeed;
            }
        }

        currentState = newState;
    }

    private void ChasingTarget()
    {
        navMeshAgent.SetDestination(Player.);
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
            Random.Range(roamingDistanceMin, roamingDistanceMax);
    }

    public void TakeDamage(float damage) => health -= damage;
}
