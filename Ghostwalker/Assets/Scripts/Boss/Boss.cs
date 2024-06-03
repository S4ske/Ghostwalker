using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Boss : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private BoxCollider2D boxCollider2D;

    [SerializeField] private Player player;
    [SerializeField] private Enemy enemy;

    [SerializeField] private int maxHealth;
    private float currentHealth;
    [SerializeField] private int speed;
    
    [SerializeField] private GameObject laserPrefab;

    [SerializeField] private float _roamingDistanceMax = 7f;
    [SerializeField] private float _roamimgDistanceMin = 3f;
    [SerializeField] private float _roamimgTimerMax = 2f;

    private float _roamingTimer;
    private Vector3 _roamPosition;
    private Vector3 _startingPosition;

    [SerializeField] private GameObject projectilePrefabFire;
    [SerializeField] private GameObject projectilePrefabIce;
    [SerializeField] private Transform[] firePoints;
    [SerializeField] private float attackRate;
    private float nextAttackTime;

    private float currentAngle; 
    [SerializeField] private float angleStep;

    private GameObject currentProjectilePrefab;
    private float projectileSwitchTimer;
    private float slowingTime;
    [SerializeField] private float slowingInterval;
    [SerializeField] private float projectileSwitchInterval;
    
    [SerializeField] private Slider healthSlider;
    
    private float abilityCooldown = 10f;
    private float abilityTimer;

    private Animator Animator;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;

        boxCollider2D = GetComponent<BoxCollider2D>();

        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        
        currentProjectilePrefab = projectilePrefabFire;
        abilityTimer = abilityCooldown;
        
        Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            Animator.SetBool("IsDie", true);
            return;
        }

        _roamingTimer -= Time.deltaTime;
        if (_roamingTimer < 0)
        {
            Roaming();
            _roamingTimer = _roamimgTimerMax;
        }

        projectileSwitchTimer -= Time.deltaTime;
        if (projectileSwitchTimer <= 0)
        {
            SwitchProjectilePrefab();
            SwitchRotation();
            projectileSwitchTimer = projectileSwitchInterval;
        }
        
        slowingTime -= Time.deltaTime;
        if (slowingTime <= 0)
        {
            player.moveSpeed = 5;
            slowingTime = slowingInterval;
        }

        if (Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + attackRate;
        }
        
        abilityTimer -= Time.deltaTime;
        if (abilityTimer <= 0)
        {
            UseLaserAbility();
            abilityTimer = abilityCooldown;
            var enemyClone = Instantiate(enemy, new Vector3(0, 0, 0), quaternion.identity);
            enemyClone.GetComponent<Enemy>().PlayerPosition = player.transform;
        }

        if (currentHealth < maxHealth / 2)
        {
            navMeshAgent.speed += 5;
            attackRate = (float)0.2;
            abilityCooldown = 7;
            _roamimgTimerMax = 2;
        }
    }

    private void Roaming()
    {
        navMeshAgent.speed = speed;
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

    private void Attack()
    {
        foreach (Transform firePoint in firePoints)
        {
            var direction = new Vector3(Mathf.Cos(currentAngle * Mathf.Deg2Rad), 
                                        Mathf.Sin(currentAngle * Mathf.Deg2Rad), 0);
            
            var projectile = Instantiate(currentProjectilePrefab, firePoint.position, Quaternion.identity);
            var projectileScript = projectile.GetComponent<Projectile>();
            if (projectileScript != null)
            {
                projectileScript.SetDirection(direction);
                projectileScript.fireType = currentProjectilePrefab;
            }
            currentAngle += angleStep;
        }
    }

    private void UseLaserAbility()
    {
        foreach (Transform firePoint in firePoints)
        {
            var laser = Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
            var laserScript = laser.GetComponent<Laser>();
            if (laserScript != null)
            {
                laserScript.RotateAroundBoss(transform);
            }
        }
    }

    private void SwitchProjectilePrefab()
    {
        currentProjectilePrefab = currentProjectilePrefab == projectilePrefabFire 
            ? projectilePrefabIce 
            : projectilePrefabFire;
    }
    
    private void SwitchRotation()
    {
        angleStep = -angleStep;
    }
    
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthSlider.value = currentHealth;
    }
}
