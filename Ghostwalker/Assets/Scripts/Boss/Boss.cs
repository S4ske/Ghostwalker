using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Boss : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private BoxCollider2D boxCollider2D;

    [SerializeField] private int health; 
    [SerializeField] private int speed; 
    
    [SerializeField] private float _roamingDistanceMax = 7f;
    [SerializeField] private float _roamimgDistanceMin = 3f;
    [SerializeField] private float _roamimgTimerMax = 2f;
    
    private float _roamingTimer;
    private Vector3 _roamPosition;
    private Vector3 _startingPosition;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;

        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (health <= 0)
        {
            // Animator set is die animation
        }
        Roaming();
    }
    
    private void Roaming() 
    {
        _startingPosition = transform.position;
        _roamPosition = GetRoamingPosition();
        navMeshAgent.SetDestination(_roamPosition);
    }
    
    private Vector3 GetRoamingPosition() 
    {
        return _startingPosition + GetRandomDir() * Random.Range(_roamimgDistanceMin, _roamingDistanceMax);
    }

    private static Vector3 GetRandomDir()
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
